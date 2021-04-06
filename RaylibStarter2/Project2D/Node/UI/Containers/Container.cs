using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

class Container : UI
{
	//position of UI is the start_pos
	private Vector2 size;
	public int top_padding;
	public int bottom_padding;
	public int left_padding;
	public int right_padding;
	protected bool expand_to_fill = true;

	//sort is used for sorting children of this Container
	public delegate void _Sort(List<UI> _elements);
	_Sort sort;

	// ------------ //
	// Constructors //
	// ------------ //
	public Container()
	{
		set_position(new Vector2());
		size = new Vector2(16, 16);
		top_padding = 0;
		right_padding = 0;
		bottom_padding = 0;
		left_padding = 0;
	}

	public Container(Vector2 _start, Vector2 _size, int _top_padding = 0, int _left_padding = 0, int _bottom_padding = 0, int _right_padding = 0)
	{
		set_position(_start);
		set_size(_size);
		set_margins(_top_padding, _right_padding, _bottom_padding, _left_padding);
	}

	// -------------- //
	// Public Methods //
	// -------------- //

	public override void _draw()
	{
		DrawRectangleLines((int)get_global_transform().GetPosition().x, (int)get_global_transform().GetPosition().y, (int)get_size().x, (int)get_size().y, new RLColor(255, 255, 255, 255));
		
		base._draw();
	}

	//Add item to the container
	public override void _add_child(UI _item)
    {
        base._add_child(_item);
        _update_items();
    }

    //Remove item from the container
    public override void _remove_child(UI _item)
    {
        base._remove_child(_item);
        _update_items();
    }

    //update the size of this container based on the size of its parent
    public override void _set_parent(UI _parent)
    {
        base._set_parent(_parent);

		if (_parent == null)
			return;
		
		if (_parent is Container container)
		{
			if (expand_to_fill)
			{
				set_position(new Vector2());
				set_size(container.get_size());
			}
		}
    }

	//Update the size of the button
	public void set_size(Vector2 _size)
	{
		size = _size;
	}

	//Get the size of this container
	public Vector2 get_size()
    {
		return size;
    }

	public void set_margins(int _top, int _right, int _bottom, int _left)
	{
		top_padding = _top;
		right_padding = _right;
		bottom_padding = _bottom;
		left_padding = _left;
	}

	public void set_sort(_Sort _sort_function)
	{
		sort = _sort_function;
	}

	public static void sort_vertically(List<UI> _elements)
	{
		var offset = new Vector2();

		for (int i = 0; i < _elements.Count; ++i)
		{
			var old_pos = _elements[i].get_local_position();
			var new_pos = old_pos + offset;

			_elements[i].set_position(new_pos);

			if (_elements[i] is Container resizable)
			{
				offset.y = resizable.get_size().y;
				resizable.expand_to_fill = false;
			}
			else
				offset.y += 1;
		}
	}

	public static void sort_horizontally(List<UI> _elements)
	{
		var offset = new Vector2();

		for (int i = 0; i < _elements.Count; ++i)
		{
			var old_pos = _elements[i].get_local_position();
			var new_pos = old_pos + offset;

			_elements[i].set_position(new_pos);

			if (_elements[i] is Container resizable)
			{
				resizable.expand_to_fill = false;
				offset.x = resizable.get_size().x;
			}
			else
				offset.x += 1;
		}
	}

	// ------------- //
	// Other Methods //
	// ------------- //

	//Update the arrangements of the items
	protected virtual void _update_items()
	{
		if (sort != null)
		{
			var my_children = get_children();
			var result = my_children;

			sort(my_children);

			for (int i = 0; i < my_children.Count; ++i)
			{

						//IF the child is a container
				if (result[i] is Container r_container && my_children[i] is Container my_container)
				{
					my_container.set_position(r_container.get_local_position());
					my_container.set_size(r_container.get_size());
					my_container.set_margins(r_container.top_padding, r_container.right_padding, r_container.bottom_padding, r_container.left_padding);
				} 
				else	//IF the child is not a container
				{
					my_children[i].set_position(result[i].get_local_position());
				}

			}
			
		}
	}
}
