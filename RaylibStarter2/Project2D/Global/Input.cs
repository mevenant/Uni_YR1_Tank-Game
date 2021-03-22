using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

static class Input
{
	//movement
	public static KeyboardKey KEY_RIGHT_1 = KeyboardKey.KEY_D;
	public static KeyboardKey KEY_UP_1 = KeyboardKey.KEY_W;
	public static KeyboardKey KEY_LEFT_1 = KeyboardKey.KEY_A;
	public static KeyboardKey KEY_DOWN_1 = KeyboardKey.KEY_S;

	//secondary movement
	public static KeyboardKey KEY_RIGHT_2 = KeyboardKey.KEY_RIGHT;
	public static KeyboardKey KEY_UP_2 = KeyboardKey.KEY_UP;
	public static KeyboardKey KEY_LEFT_2 = KeyboardKey.KEY_LEFT;
	public static KeyboardKey KEY_DOWN_2 = KeyboardKey.KEY_DOWN;

	/*public static Vector3 GetInput()
	{
		int r = IsKeyDown(KEY_RIGHT_1) ? 1 : 0;
		int l = IsKeyDown(KEY_LEFT_1) ? 1 : 0;
		int u = IsKeyDown(KEY_UP_1) ? 1 : 0;
		int d = IsKeyDown(KEY_DOWN_1) ? 1 : 0;

		//@TODO: Normalize this
		return new Vector3(r - l, d - u, 0);
	}

	public static Raylib.Vector3 VecToRayVec(Vector3 vec)
	{
		return new Raylib.Vector3(vec.x, vec.y, vec.z);
	}

	public static Vector3 RayVecToVec(Raylib.Vector3 vec)
	{
		return new Vector3(vec.x, vec.y);
	}*/
}

