Rewrite UI section of [Vectorization](https://www.drawing3d.de/Downloads.aspx), the C# implement of Potrace by Wolfgang Nagl & Peter Selinger


C version (official version) [Potrace](http://potrace.sourceforge.net/) can only convert image from bmp (or other bitmap format) to eps.

Vectorization is a application that converts images to SVG.

However, when reading its source code and trying to extract some functions for use, I find it very confusing to understand the UI section (written with winform).

So I rewrite the UI section with WPF. I will be very happy if these changes can be included in the source code by the original author.

I try to use simpler and clearer code, but there may be some bugs.