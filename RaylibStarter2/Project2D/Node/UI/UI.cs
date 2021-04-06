using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

struct Signal {
	public static string MOUSE_ENTERED = "0";
	public static string MOUSE_EXITED = "1";
	public static string MOUSE_PRESSED = "2";

	public UI source;
	public string message;

	public Signal(UI _source, string _message)
    {
		source = _source;
		message = _message;
    }
}

class UI
{
	//Collider
	protected Collider collider;

	//Signals
	List<UI> signal_connections = new List<UI>();
	protected bool is_hovered = false;

	//Node fields
	private List<UI> children = new List<UI>(); //list of children nodes
	protected	UI parent = null;                       //parent node
	protected	Colour modulate = RLColor.WHITE.ToColor();
	protected	Matrix3 local_transform = new Matrix3(true);
	private		Matrix3 global_transform = new Matrix3(true);   //updated in Update of Game.cs


	//go through each child and update the UI state based on input. e.g. mouse hover or mouse click
	public virtual void _update_state()
	{
		if (collider != null)
		{
			//CHECK HOVER

			if (is_hovered)
			{
				if (!collider.overlaps(get_vector(GetMousePosition())))
				{
					is_hovered = false;
					_on_mouse_exit();
				}
			} 
			else
			{
				if (collider.overlaps(get_vector(GetMousePosition())))
				{
					is_hovered = true;
					_on_mouse_enter();
				}
			}

			//CHECK PRESSED
			if (is_hovered && IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) _on_pressed();

		}

		foreach (UI node in children)
		{
			node._update_state();
		}

	}

	public virtual void _draw()
	{
		foreach (UI node in get_children())
		{
			node._draw();
		}
	}

	//update global transform
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

	public virtual void _on_mouse_enter()
	{
		Console.WriteLine("UI hovered");
		//emit signal that mouse entered this button
		_emit_signal(new Signal(this, Signal.MOUSE_ENTERED));
	}

	public virtual void _on_mouse_exit()
	{
		Console.WriteLine("UI unhovered");
		_emit_signal(new Signal(this, Signal.MOUSE_EXITED));
	}

	public virtual void _on_pressed()
	{
		Console.WriteLine("UI pressed");
		_emit_signal(new Signal(this, Signal.MOUSE_PRESSED));
	}

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

	// -- // -- // -- // -- // -- //
	//PUBLIC METHODS
	// -- // -- // -- // -- // -- //

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
			parent._remove_child(this);

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

	public void connect(UI node)
	{
		signal_connections.Add(node);
	}

	public void disconnect(UI node)
	{
		signal_connections.Remove(node);
	}
}

