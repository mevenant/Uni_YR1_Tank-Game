using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

//--------------------------------------------------------------
// Functions for use with custom math library
//--------------------------------------------------------------
static public class Renderer
{
	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static Vector2 ToVector2(this RLVector2 vec)
	{
		Vector2 result;
		result.x = vec.x;
		result.y = vec.y;

		return result;
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static RLVector2 ToRLVector2(this Vector2 vec)
	{
		RLVector2 result;
		result.x = vec.x;
		result.y = vec.y;

		return result;
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static RLColor ToRLColor(this Colour colour)
	{
		return new RLColor(colour.GetRed(), colour.GetGreen(), colour.GetBlue(), colour.GetAlpha());
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static Colour ToColor(this RLColor color)
	{
		Colour col = new Colour();
		col.SetRed(color.r);
		col.SetGreen(color.g);
		col.SetBlue(color.b);
		col.SetAlpha(color.a);

		return col;
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static Vector2 GetMousePosition()
	{
		RLVector2 pos = Raylib.Raylib.GetMousePosition();
		return pos.ToVector2();
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static void DrawLine(Vector2 startPos, Vector2 endPos, float thick, Colour colour)
	{
		DrawLineEx(startPos.ToRLVector2(), endPos.ToRLVector2(), thick, colour.ToRLColor());
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static void DrawCircle(Vector2 center, float radius, Colour colour)
	{
		DrawCircleV(center.ToRLVector2(), radius, colour.ToRLColor());
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static void DrawRectangle(Vector2 position, Vector2 size, Colour colour)
	{
		DrawRectangleV(position.ToRLVector2(), size.ToRLVector2(), colour.ToRLColor());
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static void DrawText(Font font, string text, Vector2 position, float fontSize, float spacing, Colour tint)
	{
		DrawTextEx(font, text, position.ToRLVector2(), fontSize, spacing, tint.ToRLColor());
	}

	//--------------------------------------------------------------
	//--------------------------------------------------------------
	public static void DrawTexture(Texture2D texture, Matrix3 transform, Colour color)
	{
		Vector2 xAxis = new Vector2();
		xAxis.x = transform.m1;
		xAxis.y = transform.m2;

		Vector2 yAxis = new Vector2();
		yAxis.x = transform.m4;
		yAxis.y = transform.m5;

		//Note - the angle of a unit circle count up when they go anticlockwise,
		//but because in Raylib land positive Y is down, a positive angle will
		//be clockwise.
		float angle = (float)Math.Atan2(xAxis.y, xAxis.x);
		float scaleX = xAxis.Magnitude();
		float scaleY = yAxis.Magnitude();

		//The portion of the image to render
		Rectangle source = new Rectangle();
		source.width = texture.width;
		source.height = texture.height;

		//Origin to rotate around
		RLVector2 origin = new RLVector2();
		origin.x = texture.width * 0.5f;
		origin.y = texture.height * 0.5f;

		//The target position and size to render at
		Rectangle destination = new Rectangle();
		destination.x = transform.m7;
		destination.y = transform.m8;
		destination.width = source.width * scaleX;
		destination.height = source.height * scaleY;

		//We multiply the angle by 180/pi because the matrix library for the project
		//works in radians (that's what the unit tests require) but RayLib expects degrees.
		float degrees = angle * 180.0f / (float)Math.PI;

		//Convert our color class to a raylib colour
		RLColor rlColor = new RLColor(color.GetRed(), color.GetGreen(), color.GetBlue(), color.GetAlpha());

		//Draw texture
		DrawTexturePro(texture, source, destination, origin, degrees, rlColor);
	}
}
