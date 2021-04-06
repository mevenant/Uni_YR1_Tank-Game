using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

public delegate void _Action();
class Button : Container
{
	//this happens when the button is pressed
	public _Action _action;
	static string SIGNAL_CHANGE_TEXTURE_TO_WALL = "0";

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
	//Note that size and position of this button will be overriden if it's a child of a container
	public Button(string _text, Vector2 _position, Vector2 _size, RLColor _color_background, RLColor _color_text, RLColor _color_border)
	{
		set_position(_position);
		set_size(_size);

		text = _text;

		color_normal = _color_background;
		color_text = _color_text;
		color_border = _color_border;

		color_background = color_normal;
	}

	public Button(Vector2 _position, Texture2D _texture)
	{
		is_textured = true;
		expand_to_fill = false;
		texture = _texture;
		
		set_position(_position);
		set_size(new Vector2(texture.width, texture.height));

		text = "";
	}

	public override void _update_global_transform()
	{
		base._update_global_transform();

		Vector2 min;
		Vector2 max;

		//apply margins or not
		if (expand_to_fill)
		{
			min = new Vector2(-get_size().x * 0.5f + left_padding, -get_size().y * 0.5f + top_padding);
			max = new Vector2(get_size().x * 0.5f - right_padding, get_size().y * 0.5f - bottom_padding);
		}
		else
		{
			min = new Vector2(-get_size().x * 0.5f, -get_size().y * 0.5f);
			max = new Vector2(get_size().x * 0.5f, get_size().y * 0.5f);
		}
		

		if (collider != null)
		{
			if (collider is BoxCollider box)
			{
				box.set_bbox(min, max);
			}
			
			collider.set_position(get_global_position() + get_size() * 0.5f);
		} 
		else
		{
			collider = new BoxCollider(min, max);

			collider.set_position(get_global_position() + get_size() * 0.5f);
		}
	}

	// -- // -- //
	//   DRAW	//
	// -- // -- //
	public override void _draw()
	{
		var x = (int)get_global_position().x + left_padding;
		var y = (int)get_global_position().y + top_padding;
		var size_x = (int)get_size().x - right_padding - left_padding;
		var size_y = (int)get_size().y - bottom_padding - top_padding;

		if (!is_textured)
		{

			//Draw background
			DrawRectangle(x, y, size_x, size_y, color_background);

			//Draw border
			DrawRectangleLines(x, y, size_x, size_y, color_border);

			//Draw text
			var text_width = text.Length * 2f;
			var text_offset = get_size() * 0.5f + new Vector2(-text_width * 2f, -font_size * 0.5f);
			DrawText(text, x + (int)text_offset.x, y + (int)text_offset.y, font_size, color_text);
		} 
		else
		{
			var transform = get_global_transform();
			transform.SetPosition(transform.GetPosition() + get_size() * 0.5f);
			Renderer.DrawTexture(texture, transform, modulate);

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
		if (_action != null)
			_action();
	}

	public void set_texture(Texture2D _texture)
	{
		is_textured = true;
		texture = _texture;
	}

	public Texture2D get_texture()
    {
		return texture;
    }
}

