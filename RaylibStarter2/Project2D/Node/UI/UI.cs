using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

class UI
{
	//Collider
	protected Collider collider;

	//Signals
	protected bool is_hovered = false;

	//Node fields
	protected	List<UI> children = new List<UI>(); //list of children nodes
	protected	UI parent = null;                       //parent node
	protected	Colour modulate = RLColor.WHITE.ToColor();
	protected	Matrix3 local_transform = new Matrix3(true);
	private		Matrix3 global_transform = new Matrix3(true);   //updated in Update of Game.cs

	public UI(UI _parent)
	{
		set_parent(_parent);
	}

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
	public virtual void update_global_transform()
	{
		if (parent != null)
			global_transform = parent.global_transform * local_transform;
		else
			global_transform = local_transform;

		if (collider != null)
			collider.set_position(get_global_position());


		foreach (UI node in children)
		{
			node.update_global_transform();
		}
	}

	public virtual void _on_mouse_enter()
	{
		//Console.WriteLine("UI hovered");
	}

	public virtual void _on_mouse_exit()
	{
		//Console.WriteLine("UI unhovered");
	}

	public virtual void _on_pressed()
	{
		//Console.WriteLine("UI pressed");
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
	public void add_child(UI _node)
	{
		children.Add(_node);
	}

	//Remove a given node from the children of this node
	public void remove_child(UI _node)
	{
		children.Remove(_node);
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
	public void set_parent(UI _parent)
	{
		//if this node is a child of another parent, remove this from their list of children
		if (parent != null)
			parent.remove_child(this);

		//update current parent and its list of children
		parent = _parent;

		if (parent != null)
			parent.add_child(this);
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
}

