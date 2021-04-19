using Raylib;
using static Raylib.Raylib;
using MathClasses;

// ---------------------------------- //
// used to centeralize input handling //
// ---------------------------------- //

static class Input
{
	//primary movement
	public static KeyboardKey KEY_RIGHT_1 = KeyboardKey.KEY_D;
	public static KeyboardKey KEY_UP_1 = KeyboardKey.KEY_W;
	public static KeyboardKey KEY_LEFT_1 = KeyboardKey.KEY_A;
	public static KeyboardKey KEY_DOWN_1 = KeyboardKey.KEY_S;

	//secondary movement
	public static KeyboardKey KEY_RIGHT_2 = KeyboardKey.KEY_RIGHT;
	public static KeyboardKey KEY_UP_2 = KeyboardKey.KEY_UP;
	public static KeyboardKey KEY_LEFT_2 = KeyboardKey.KEY_LEFT;
	public static KeyboardKey KEY_DOWN_2 = KeyboardKey.KEY_DOWN;

	// -- // -- //
	//Return a Vector3 based on player's input
	// -- // -- //
	public static Vector2 get_primary_input()
	{
		int r = IsKeyDown(KEY_RIGHT_1) ? 1 : 0;
		int l = IsKeyDown(KEY_LEFT_1) ? 1 : 0;
		int u = IsKeyDown(KEY_UP_1) ? 1 : 0;
		int d = IsKeyDown(KEY_DOWN_1) ? 1 : 0;

		var result = new Vector2(r - l, d - u);
		result.Normalize();
		return result;
	}

	public static Vector2 get_secondary_input()
	{
		int r = IsKeyDown(KEY_RIGHT_2) ? 1 : 0;
		int l = IsKeyDown(KEY_LEFT_2) ? 1 : 0;
		int u = IsKeyDown(KEY_UP_2) ? 1 : 0;
		int d = IsKeyDown(KEY_DOWN_2) ? 1 : 0;

		var result = new Vector2(r - l, d - u);
		result.Normalize();
		return result;
	}
}

