# EfCoreCompileBench

Results for really simple empty project: https://github.com/progressonderwijs/EfCoreCompileBench/commit/a4ec6c9fd3320131cb97a8279bd38992d3b89c96
```
Release310Rebuild: 0.7990629899999999 +/- 0.009089836825097489 seconds (including best time 0.7840217; excluding outliers 0.8307335; 0.8280571)

Release311Rebuild: 1.20749151 +/- 0.009874841714523908 seconds (including best time 1.1859872; excluding outliers 1.225968; 1.2197148)

Debug310Rebuild: 0.80531395 +/- 0.010476545098480696 seconds (including best time 0.779024; excluding outliers 0.8275574; 0.818614)

Debug311Rebuild: 1.20266026 +/- 0.01071111190112402 seconds (including best time 1.1865318; excluding outliers 1.228329; 1.2187895)

Debug310NoOpBuild: 0.66530095 +/- 0.013576504062184087 seconds (including best time 0.6419545; excluding outliers 0.6876717; 0.6875402)

Debug311NoOpBuild: 0.66312895 +/- 0.009086628069559155 seconds (including best time 0.6493933; excluding outliers 0.687344; 0.6785388)

Debug310BuildWithTouchedFile: 0.70639161 +/- 0.011510180214614352 seconds (including best time 0.6856614; excluding outliers 0.7343375; 0.7254502)

Debug311BuildWithTouchedFile: 1.11351753 +/- 0.01382988313790468 seconds (including best time 1.0830584; excluding outliers 1.1413067; 1.13962)
```


Results for really simple project with a few pocos and nothing else: https://github.com/progressonderwijs/EfCoreCompileBench/commit/815169276490d481ce19a1b86192a135bef92f9e

```
Release310Rebuild: 0.85978165 +/- 0.009124532855467174 seconds (including best time 0.8433408; excluding outliers 0.9317189; 0.8914965)

Release311Rebuild: 1.35740196 +/- 0.009594697727411831 seconds (including best time 1.3351364; excluding outliers 1.3892793; 1.3746189)

Debug310Rebuild: 0.8402118900000001 +/- 0.013946569362710699 seconds (including best time 0.8193622; excluding outliers 0.859393; 0.8593222)

Debug311Rebuild: 1.36552608 +/- 0.007330033102762905 seconds (including best time 1.3538635; excluding outliers 1.3824205; 1.3798759)

Debug310NoOpBuild: 0.6769775199999999 +/- 0.01140954118085382 seconds (including best time 0.6574034; excluding outliers 0.7032221; 0.7030691)

Debug311NoOpBuild: 0.6751024200000001 +/- 0.016980186497668436 seconds (including best time 0.653412; excluding outliers 0.7031888; 0.7030534)

Debug310BuildWithTouchedFile: 0.75654405 +/- 0.014280797743421067 seconds (including best time 0.7305446; excluding outliers 0.787479; 0.7811501)

Debug311BuildWithTouchedFile: 1.2554106999999999 +/- 0.01459606308029658 seconds (including best time 1.2357788; excluding outliers 1.3029525; 1.2813012)
```
