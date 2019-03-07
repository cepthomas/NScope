# NebScope
- UDP assumes all little-endian.
- Newtonsoft.Json is version 9.0.1 - don't update.

# Graphics Processing
- The app is all WinForms using [SkiaSharp](https://github.com/mono/SkiaSharp) which is a major improvement over GDI. Note that if you create an app with SkiaSharp, be sure to uncheck the Build config box "Prefer 32 bit".
- Also [this](https://github.com/mono/SkiaSharp/issues/190).
