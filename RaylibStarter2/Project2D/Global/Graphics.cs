using Raylib;
using static Raylib.Raylib;

// ---------------------------------------------- //
// This is used to centeralize access of textures //
// ---------------------------------------------- //

static class Graphics
{
	// paths to textures //

	public static string tank_body = "../Graphics/sprTank_Body.png";
	public static string tank_turret = "../Graphics/sprTank_Turret.png";
	public static string wall = "../Graphics/sprWall.png";
	public static string empty_grid = "../Graphics/sprEmptyGrid.png";

	// pre loaded textures //

	public static Texture2D texture_wall = get_texture_from_path(wall);
	public static Texture2D texture_tank = get_texture_from_path(tank_body);
	public static Texture2D texture_empty_grid = get_texture_from_path(empty_grid);

	// create a texture based on path and return it //

	public static Texture2D get_texture_from_path(string _path)
	{
		Image image = LoadImage(_path);
		var result	= LoadTextureFromImage(image);
		UnloadImage(image);

		return result;
	}

}

