using System;
using System.Collections.Generic;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

// --------------------------------------- //
// Used to handle signals between UI nodes //
// --------------------------------------- //

struct Signal {
	public static string MOUSE_ENTERED = "0";
	public static string MOUSE_EXITED = "1";
	public static string MOUSE_PRESSED = "2";
	public static string MOUSE_PRESSED_SECONDARY = "3";

	public UI source;
	public string message;

	public Signal(UI _source, string _message)
    {
		source = _source;
		message = _message;
    }
}

// ------------------------------------ //
// Used to update the theme of UI nodes //
// ------------------------------------ //

class Theme
{
	public RLColor color_primary;
	public RLColor color_secondary;
	public RLColor color_text;
	public RLColor color_hover;

	public Theme(RLColor _primary_color, RLColor _secondary_color, RLColor _text_color, RLColor _hover_color)
	{
		color_primary = _primary_color;
		color_secondary = _secondary_color;
		color_text = _text_color;
		color_hover = _hover_color;
	}
}

// ------------------------- //
// The base of every UI node //
// ------------------------- //

class UI
{
	//Collider
	protected Collider collider;

	//Signals
	List<UI> signal_connections = new List<UI>();
	protected bool is_hovered = false;

	//Node fields
	private List<UI> children = new List<UI>(); //list of children nodes
	protected UI parent = null;                       //parent node
	protected Colour modulate = RLColor.WHITE.ToColor();
	protected Matrix3 local_transform = new Matrix3(true);
	private	Matrix3 global_transform = new Matrix3(true);   //updated in Update of Game.cs

	protected Theme theme = new Theme(RLColor.BROWN, RLColor.RED, RLColor.WHITE, RLColor.RED);

	// ----------- //
	// CONSTRUCTOR //
	// ----------- //

	public UI() { }


	// ------------------------------------------------------------------------------------------- //
	//go through each child and update the UI state based on input. e.g. mouse hover or mouse click

	public virtual void _update_state()
	{
		if (collider != null)
		{
			if (is_hovered)
			{
				if (!collider.overlaps(get_vector(GetMousePosition())))		//mouse exited
				{
					is_hovered = false;
					_on_mouse_exit();
				}

				if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) _on_pressed();
				if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON)) _on_pressed_secondary();
			} 
			else
			{
				if (collider.overlaps(get_vector(GetMousePosition())))		//mouse entered
				{
					is_hovered = true;
					_on_mouse_enter();
				}

			}
		}

		foreach (UI node in children)
		{
			node._update_state();
		}

	}

	// ---- //
	// DRAW //
	// ---- //

	public virtual void _draw()
	{
		foreach (UI node in get_children())
		{
			node._draw();
		}
	}

	// ----------------------- //
	// update global transform //

	public virtual void _update_global_transform()
	{
		if (parent != null)
			global_transform = parent.global_transform * local_transform;
		else
			global_transform = local_transform;

		if (collider != null)
			collider.set_position(get_global_position());


		foreach (UI node in children)
		{
			node._update_global_transform();
		}
	}

	// ------- //
	// SIGNALS //
	// ------- //

	// ----------- //
	// mouse enter //
	
	public virtual void _on_mouse_enter()
	{
		if (Global.IS_DEBUG)
			Console.WriteLine("UI hovered");
		_emit_signal(new Signal(this, Signal.MOUSE_ENTERED));
	}

	// ---------- //
	// mouse exit //

	public virtual void _on_mouse_exit()
	{
		if (Global.IS_DEBUG)
			Console.WriteLine("UI unhovered");
		_emit_signal(new Signal(this, Signal.MOUSE_EXITED));
	}

	// ------------- //
	// mouse pressed //

	public virtual void _on_pressed()
	{
		if (Global.IS_DEBUG)
			Console.WriteLine("UI pressed");
		_emit_signal(new Signal(this, Signal.MOUSE_PRESSED));
	}

	// ----------------------  //
	// mouse pressed secondary //

	public virtual void _on_pressed_secondary()
	{
		if (Global.IS_DEBUG)
			Console.WriteLine("UI pressed secondary");
		_emit_signal(new Signal(this, Signal.MOUSE_PRESSED_SECONDARY));
	}

	// --------------------- //
	// PUBLIC STATIC METHODS //
	// --------------------- //

	public static RLVector2 get_RLVector2(Vector2 _vector)
	{
		RLVector2 result = new RLVector2();
		result.x = _vector.x;
		result.y = _vector.y;
		return result;
	}

	public static Vector2 get_vector(RLVector2 _vector)
	{
		Vector2 result = new Vector2();
		result.x = _vector.x;
		result.y = _vector.y;
		return result;
	}

	// -------------- //
	// PUBLIC METHODS //
	// -------------- //

	//Add a node as a child of this node
	public virtual void _add_child(UI _node)
	{
		children.Add(_node);
		_node._set_parent(this);
	}

	//Remove a given node from the children of this node
	public virtual void _remove_child(UI _node)
	{
		children.Remove(_node);
		_node._set_parent(null);
	}

	//Get parent of this node
	public UI get_parent()
	{
		return parent;
	}

	//Get the list of children of this node
	public List<UI> get_children()
	{
		return children;
	}

	//This is enough to change the parent of a node
	public virtual void _set_parent(UI _parent)
	{
		if (_parent.get_children().Contains(this))
			return;

		//if this node is a child of another parent, remove this from their list of children
		if (parent != null)
		{
			parent._remove_child(this);
			theme = parent.theme;
		}

		//update current parent and its list of children
		parent = _parent;

		if (parent != null)
			parent._add_child(this);
	}

	//update local transform based on the given Vector2
	public void set_position(Vector2 _vec)
	{
		local_transform.SetPosition(_vec);
	}

	public Vector2 get_local_position()
	{
		return local_transform.GetPosition();
	}

	public Vector2 get_global_position()
	{
		return global_transform.GetPosition();
	}

	public void set_rotation(float _radians)
	{
		Matrix3 rotation = new Matrix3(true);
		rotation.SetRotateZ(_radians);

		local_transform = local_transform * rotation;
	}

	public Matrix3 get_global_transform()
	{
		return global_transform;
	}

	//Recieve signal
	public virtual void _recieve_signal(Signal _signal)
    { }

	//Emit signal to connections
	public virtual void _emit_signal(Signal _signal)
    {
		foreach (UI node in signal_connections)
        {
			node._recieve_signal(_signal);
        }
    }

	//Connect this node to _node. So we send signals to them
	public void connect(UI _node)
	{
		signal_connections.Add(_node);
	}

	//Disconnect this node from _node so we don't send signals to them anymore
	public void disconnect(UI node)
	{
		signal_connections.Remove(node);
	}

	// set the theme of this ui node and its children //

	public void set_theme(Theme _theme)
	{
		theme = _theme;

		foreach (UI child in children)
        {
			set_theme(theme);
        }
	}
}

