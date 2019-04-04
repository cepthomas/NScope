# NebScope
- This is sparse doc, more later. You'll have to read the code.
- Displays the data you feed it, similar to an oscilloscope. Two channels.
- Use the form directly in your project and call UpdateData(int channel, int cmd, double[] data).
- Run the app separately and feed it via UDP. Note that UDP assumes all little-endian.
- Newtonsoft.Json is version 9.0.1 - don't update.
- The app is all WinForms using [SkiaSharp](https://github.com/mono/SkiaSharp) which is a major improvement over GDI. Note that if you create an app with SkiaSharp, be sure to uncheck the Build config box "Prefer 32 bit". Also [read this](https://github.com/mono/SkiaSharp/issues/190).
