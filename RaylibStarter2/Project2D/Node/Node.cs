using System.Collections.Generic;
using Raylib;
using MathClasses;

// ------------------------- //
// Base of every game object //
// ------------------------- //

class Node
{
	// -- // -- // -- // -- //
	//FIELDS
	// -- // -- // -- // -- //
		
	protected	List<Node> children = new List<Node>(); //list of children nodes
	protected	Node parent = null;						//parent node
	protected	Texture2D texture;
	protected	Colour modulate = RLColor.WHITE.ToColor();
	protected	Matrix3 local_transform = new Matrix3(true);
	private		Matrix3 global_transform = new Matrix3(true);   //updated in Update of Game.cs
	protected bool deleted = false;

	// -- // -- // -- // -- //
	//CONSTRUCTORS
	// -- // -- // -- // -- //
	public Node(Node _parent, string _texture_path = "")
	{
		set_parent(_parent);
		if (_texture_path != null && !_texture_path.Equals(""))
			set_texture(Graphics.get_texture_from_path(_texture_path));
	}

	// -- // -- // -- // -- // -- //
	//VIRTUAL METHODS
	// -- // -- // -- // -- // -- //

	//called everytime the node should be drawn
	public virtual void _draw()
	{
		if (deleted)
			return;

		Renderer.DrawTexture(texture, global_transform, modulate);

		foreach (Node node in get_children())
		{
			node._draw();
		}
	}


	// -- // -- // -- // -- // -- //
	//PUBLIC METHODS
	// -- // -- // -- // -- // -- //

	//Add a node as a child of this node
	public void add_child(Node _node)
	{
		children.Add(_node);
	}

	//Remove a given node from the children of this node
	public void remove_child(Node _node)
	{
		children.Remove(_node);
	}

	//Get parent of this node
	public Node get_parent()
	{
		return parent;
	}

	//Get the list of children of this node
	public List<Node> get_children()
	{
		return children;
	}

	//This is enough to change the parent of a node
	public void set_parent(Node _parent)
	{
		if (deleted)
			return;

		//if this node is a child of another parent, remove this from their list of children
		if (parent != null)
			parent.remove_child(this);

		//update current parent and its list of children
		parent = _parent;

		if (parent != null)
			parent.add_child(this);
	}

	// update global transform //

	public virtual void _update_global_transform()
	{
		if (deleted)
			return;

		if (parent != null)
			global_transform = parent.global_transform * local_transform;
		else
			global_transform = local_transform;

		foreach (Node node in children)
		{
			node._update_global_transform();
		}
	}

	// update local transform based on the given Vector2 //
	public void set_position(Vector2 _vec)
	{
		local_transform.SetPosition(_vec);
	}

	// get local position //

	public Vector2 get_local_position()
	{
		return local_transform.GetPosition();
	}

	// get global position //

	public Vector2 get_global_position()
	{
		return global_transform.GetPosition();
	}

	// rotate the transform of this node relatively //

	public void rotate(float _radians)
	{
		Matrix3 rotation = new Matrix3(true);
		rotation.SetRotateZ(_radians);

		local_transform = local_transform * rotation;
	}

	// update the texture of this node //

	public void set_texture(Texture2D _texture)
	{
		texture = _texture;
	}

	// get the global transform of this node //

	public Matrix3 get_global_transform()
	{
		return global_transform;
	}

	// delete this node and its children //
	public void free()
	{
		foreach (Node node in children)
		{
			node.free();
		}
	}

	// use to clamp //

	public static float clamp(float _value, float _min, float _max)
	{
		if (_value < _min)
			_value = _min;
		else if (_value > _max)
			_value = _max;

		return _value;
	}

	
}

