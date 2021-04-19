using Raylib;
using static Raylib.Raylib;
using MathClasses;

public delegate void _Action();
class Button : Container
{
	//this happens when the button is pressed
	protected _Action action;

	string text;
	int font_size = 16;

	Texture2D texture;

	bool is_textured = false;

	// -- // --  //
	//CONSTRUCTOR// Note that size and position of this button will be overriden if it's a child of a container
	// -- // --  //
	public Button(string _text, Vector2 _position, Vector2 _size)
	{
		set_position(_position);
		set_size(_size);
		text = _text;
		expand_to_fill = true;
	}

	public Button(Vector2 _position, Texture2D _texture)
	{
		is_textured = true;
		texture = _texture;
		
		set_position(_position);
		set_size(new Vector2(texture.width, texture.height));

		text = "";
		expand_to_fill = false;
	}

	// ------------------------------------------- //
	// UPDATE GLOBAL TRANSFORM AND COLLIDER'S SIZE //
	// ------------------------------------------- //
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
			DrawRectangle(x, y, size_x, size_y, is_hovered ? theme.color_hover : theme.color_primary);

			//Draw border
			DrawRectangleLines(x, y, size_x, size_y, theme.color_secondary);

			//Draw text
			var text_width = text.Length * 2f;
			var text_offset = get_size() * 0.5f + new Vector2(-text_width * 2f, -font_size * 0.5f);
			DrawText(text, x + (int)text_offset.x, y + (int)text_offset.y, font_size, theme.color_text);
		} 
		else
		{
			var transform = get_global_transform();
			transform.SetPosition(transform.GetPosition() + get_size() * 0.5f);
			Renderer.DrawTexture(texture, transform, modulate);

		}

		base._draw();
	}

	// -------------- //
	// PUBLIC METHODS //
	// -------------- //

	public void set_texture(Texture2D _texture)
	{
		is_textured = true;
		texture = _texture;
	}

	public Texture2D get_texture()
	{
		return texture;
	}

	public void set_action(_Action _action)
	{
		action = _action;
	}

	// -- // -- //
	//	SIGNALS //
	// -- // -- //

	public override void _on_pressed()
	{
		base._on_pressed();
		if (action != null)
			action();
	}

	
}

