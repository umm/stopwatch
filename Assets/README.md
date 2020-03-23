# stopwatch

## What

- StopWatch logic based on unity frame updates

## Requirement

- unirx
- extra\_unirx

## Install

### With Unity Package Manager

```bash
upm add package dev.upm-packages.stopwatch
```

Note: `upm` command is provided by [this repository](https://github.com/upm-packages/upm-cli).

You can also edit `Packages/manifest.json` directly.

```jsonc
{
  "dependencies": {
    // (snip)
    "dev.upm-packages.stopwatch": "[latest version]",
    // (snip)
  },
  "scopedRegistries": [
    {
      "name": "Unofficial Unity Package Manager Registry",
      "url": "https://upm-packages.dev",
      "scopes": [
        "dev.upm-packages"
      ]
    }
  ]
}
```

### Any other else (classical umm style)

```shell
yarn add "umm/stopwatch#^1.0.0"
```

## Usage

```csharp
var stopwatch = new StopWatch();
stopwatch.TimeAsObservable.Subscribe(it => UnityEngine.Debug.LogFormat("time: {0}", it);

stopwatch.Start();
stopwatch.Pause();
stopwatch.Resume();
stopwatch.Stop();
```

It's easy to inject clocking.

```
var oscillator = new Subject<float>();
var stopwatch = new StopWatch(oscillator);

oscillator.OnNext(1f); // 1f second passed
```

## License

Copyright (c) 2018 Takuma Maruyama

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)
