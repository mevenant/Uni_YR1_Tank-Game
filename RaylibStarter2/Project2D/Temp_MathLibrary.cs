using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Placeholder Matrix3, Vector2, and Color class. Remove these after you've added your own.
public class Matrix3
{
	public float m1;
	public float m2;
	public float m3;
	public float m4;
	public float m5;
	public float m6;
	public float m7;
	public float m8;
	public float m9;
}

public struct Vector2
{
	public float x;
	public float y;

	public float Magnitude()
	{
		return (float)Math.Sqrt((x * x) + (y * y));
	}
}

public class Colour
{
	public void SetRed(byte value) { }
	public void SetGreen(byte value) { }
	public void SetBlue(byte value) { }
	public void SetAlpha(byte value) { }

	public byte GetRed()
	{
		return 255;
	}
	public byte GetGreen()
	{
		return 255;
	}
	public byte GetBlue()
	{
		return 255;
	}
	public byte GetAlpha()
	{
		return 255;
	}
}