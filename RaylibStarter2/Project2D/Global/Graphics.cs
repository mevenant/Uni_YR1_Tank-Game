using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

static class Graphics
{
	public static string tank_body = "../Graphics/sprTank_Body.png";
	public static string tank_turret = "../Graphics/sprTank_Turret.png";

	public static Texture2D get_texture_from_path(string _path)
	{
		Image image = LoadImage(_path);
		var result	= LoadTextureFromImage(image);
		UnloadImage(image);

		return result;
	}
}

