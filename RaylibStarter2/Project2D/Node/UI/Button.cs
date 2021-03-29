using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

class Button : UI
{
	public delegate void _Action();

	//this happens when the button is pressed
	public _Action _action;

	//private fields
	Vector2 size;
	string text;
	int font_size = 16;
	RLColor color_hover = RLColor.RED;
	RLColor color_normal = RLColor.GOLD;
	
	RLColor color_background;
	RLColor color_border;
	RLColor color_text;

	Texture2D texture;

	bool is_textured = false;
	// -- // --  //
	//CONSTRUCTOR//
	// -- // --  //
	public Button(UI _parent, string _text, Vector2 _position, RLColor _color_background, RLColor _color_text, RLColor _color_border, float _size_x = 64, float _size_y = 32, string _texture_path = "") : base(_parent)
	{
		local_transform.SetPosition(_position);
		size = new Vector2(_size_x, _size_y);
		
		collider = new BoxCollider(
			new Vector2(0, 0),
			new Vector2(size.x, size.y));

		text = _text;

		color_normal = _color_background;
		color_text = _color_text;
		color_border = _color_border;

		color_background = color_normal;
	}

	public Button(UI _parent, string _text, Vector2 _position, Texture2D _texture) : base(_parent)
	{
		local_transform.SetPosition(_position);

		is_textured = true;
		texture = _texture;
		size = new Vector2(texture.width, texture.height);

		collider = new BoxCollider(
			new Vector2(-size.x/2, -size.y/2),
			new Vector2(size.x/2, size.y/2));

		text = _text;
	}

	// -- // -- //
	//   DRAW	//
	// -- // -- //
	public override void _draw()
	{
		if (!is_textured)
		{
			//Draw background
			DrawRectangle((int)get_global_position().x, (int)get_global_position().y, (int)size.x, (int)size.y, color_background);

			//Draw border
			DrawRectangleLines((int)get_global_position().x, (int)get_global_position().y, (int)size.x, (int)size.y, color_border);

			//Draw text
			var text_width = text.Length * 2f;
			var text_offset = size * 0.5f + new Vector2(-text_width * 2f, -font_size * 0.5f);
			DrawText(text, (int)(get_global_position().x + text_offset.x), (int)(get_global_position().y + text_offset.y), font_size, color_text);
		} else
		{
			Renderer.DrawTexture(texture, get_global_transform(), modulate);
		}

		base._draw();
	}

	// -- // -- //
	//	SIGNALS //
	// -- // -- //

	public override void _on_mouse_enter()
	{
		base._on_mouse_enter();
		color_background = color_hover;
	}

	public override void _on_mouse_exit()
	{
		base._on_mouse_exit();
		color_background = color_normal;
	}

	public override void _on_pressed()
	{
		base._on_pressed();
		_action();
	}

	public void set_texture(Texture2D _texture)
	{
		is_textured = true;
		texture = _texture;
	}
}

