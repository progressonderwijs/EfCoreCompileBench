using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using IncrementalMeanVarianceAccumulator;

namespace Benchmark
{
    class Program
    {
        const int NumberOfBuildsToBenchmark = 12;
        const int SlowOutliersToDiscard = 2; //at least one, for caching effects.

        static void Main()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;

            var results = (
                from build in PredefinedBuilds.Builds
                let times = Enumerable.Repeat(0, NumberOfBuildsToBenchmark).Select(_ => TimeBuild(build)).ToArray()
                let worstToBest = times.OrderByDescending(s => s).ToArray()
                let distributionWithoutOutliers = MeanVarianceAccumulator.FromSequence(worstToBest.Skip(SlowOutliersToDiscard))
                let ignoredOutliers = worstToBest.Take(SlowOutliersToDiscard)
                let best = worstToBest.Last()
                select $"{build.Name}: {distributionWithoutOutliers} seconds (including best time {best}; excluding outliers {string.Join("; ", ignoredOutliers)})"
            ).ToArray();

            Console.WriteLine(string.Join("\r\n\r\n", results));
        }

        static readonly Uri solutionpath = GetMyPath();

        static Uri GetMyPath([CallerFilePath] string path = null)
            => new Uri(new Uri(path), @"..\");

        static class PredefinedBuilds
        {
            public static readonly BuildSettings[] Builds = {
                new BuildSettings("Release311Rebuild") { Executable = "dotnet", Arguments = "msbuild Project311/Project311.csproj -target:restore;rebuild -verbosity:minimal -p:configuration=Release" },
                new BuildSettings("Release310Rebuild") { Executable = "dotnet", Arguments = "msbuild Project310/Project310.csproj -target:restore;rebuild -verbosity:minimal -p:configuration=Release" },

                new BuildSettings("Debug311Rebuild") { Executable = "dotnet", Arguments = "msbuild Project311/Project311.csproj -target:restore;rebuild -verbosity:minimal -p:configuration=Debug" },
                new BuildSettings("Debug310Rebuild") { Executable = "dotnet", Arguments = "msbuild Project310/Project310.csproj -target:restore;rebuild -verbosity:minimal -p:configuration=Debug" },

                new BuildSettings("Debug311NoOpBuild") { Executable = "dotnet", Arguments = "msbuild Project311/Project311.csproj -target:restore;build -verbosity:minimal -p:configuration=Debug" },
                new BuildSettings("Debug310NoOpBuild") { Executable = "dotnet", Arguments = "msbuild Project310/Project310.csproj -target:restore;build -verbosity:minimal -p:configuration=Debug" },

                new BuildSettings("Debug311BuildWithTouchedFile") {
                    Prebuild = FileToucher("Project311/Program.cs"),
                    Executable = "dotnet",
                    Arguments = "msbuild Project311/Project311.csproj -target:restore;build -verbosity:minimal -p:configuration=Debug"
                },
                new BuildSettings("Debug310BuildWithTouchedFile") {
                    Prebuild = FileToucher("Project310/Program.cs"),
                    Executable = "dotnet",
                    Arguments = "msbuild Project310/Project310.csproj -target:restore;build -verbosity:minimal -p:configuration=Debug"
                },
            };

            static Action FileToucher(string solutionRelativeFilePath)
                => () => File.SetLastWriteTimeUtc(new Uri(solutionpath, solutionRelativeFilePath).LocalPath, DateTime.UtcNow);
        }

        class BuildSettings
        {
            public readonly string Name;

            public BuildSettings(string name)
                => Name = name;

            public string Executable;
            public string Arguments;
            public Action Prebuild;

            public override string ToString()
                => Name;
        }

        static double TimeBuild(BuildSettings settings)
        {
            settings.Prebuild?.Invoke();
            var sw = Stopwatch.StartNew();
            var startInfo = new ProcessStartInfo {
                WorkingDirectory = solutionpath.LocalPath,
                FileName = settings.Executable,
                Arguments = settings.Arguments,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };
            using var proc = Process.Start(startInfo);
            proc.StandardInput.Close();
            proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return sw.Elapsed.TotalSeconds;
        }
    }
}
