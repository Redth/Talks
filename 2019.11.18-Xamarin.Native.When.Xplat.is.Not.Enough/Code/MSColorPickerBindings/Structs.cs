using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using UIKit;

[StructLayout(LayoutKind.Sequential)]
public struct RGB
{
	public nfloat red;

	public nfloat green;

	public nfloat blue;

	public nfloat alpha;
}

[StructLayout(LayoutKind.Sequential)]
public struct HSB
{
	public nfloat hue;

	public nfloat saturation;

	public nfloat brightness;

	public nfloat alpha;
}

[Native]
public enum MSSelectedColorView : ulong
{
	Rgb,
	Hsb
}
