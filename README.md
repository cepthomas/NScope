# NScope

A toy oscilloscope for experimenting with the Skia graphics lib. The doc is sparse, you'll have to read the code.

- Displays the data you feed it, similar to an oscilloscope. Two channels.
- Use the form directly in your project and call UpdateData(int channel, int cmd, double[] data).
- Run the app separately and feed it via UDP. Note that UDP assumes all little-endian.
- The app is all WinForms using [SkiaSharp](https://github.com/mono/SkiaSharp) which is a major improvement over GDI.
  Note that if you create an app with SkiaSharp, be sure to uncheck the Build config box "Prefer 32 bit".
  Also [read this](https://github.com/mono/SkiaSharp/issues/190).
- Requires VS2022 and .NET6.


# External Components

- Application icon: [Charlotte Schmidt](http://pattedemouche.free.fr/) (Copyright © 2009 of Charlotte Schmidt).
- Button icons: [Glyphicons Free](http://glyphicons.com/) (CC BY 3.0).
- [SkiaSharp](https://github.com/mono/SkiaSharp) (MIT)
