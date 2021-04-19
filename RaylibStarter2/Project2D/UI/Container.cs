using System;
using System.Collections.Generic;
using MathClasses;
using Raylib;
using static Raylib.Raylib;

// -------------------------------------------------------------------------------------------------------------------------- //
// containers are used to contain, sort, and manage child elements. They are fully functional with padding and custom sorting //
// -------------------------------------------------------------------------------------------------------------------------- //

class Container : UI
{
	//size
	private Vector2 size;
	protected bool expand_to_fill = true;

	//padding
	public int top_padding;
	public int bottom_padding;
	public int left_padding;
	public int right_padding;

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
		set_padding(_top_padding, _right_padding, _bottom_padding, _left_padding);
	}

	// -------------- //
	// Public Methods //
	// -------------- //

	public override void _draw()
	{
		if (Global.IS_DEBUG)
			DrawRectangleLines((int)get_global_transform().GetPosition().x, (int)get_global_transform().GetPosition().y, (int)get_size().x, (int)get_size().y, RLColor.RED);

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

	// update the padding of this element // 

	public void set_padding(int _top, int _right, int _bottom, int _left)
	{
		top_padding = _top;
		right_padding = _right;
		bottom_padding = _bottom;
		left_padding = _left;
	}

	// update the sorting method of this container, effective immedietly //

	public void set_sort(_Sort _sort_function)
	{
		sort = _sort_function;
		_update_items();
	}

	

	// Update the arrangements of the items //
	protected virtual void _update_items()
	{
		if (sort != null)
		{
			//sort items based on the provided function
			var my_children = get_children();
			sort(my_children);

			//additional arrangements of items
			for (int i = 0; i < my_children.Count; ++i)
			{
						//IF the child is a container
				if (my_children[i] is Container my_container)
				{
					if (my_container.expand_to_fill)		// expand child
					{
						//set_position(new Vector2());
						//set_size(container.get_size());
						my_container.expand_to_fill = false;
					}
				}
			}
			
		}
	}

	// ---------------------------------------------- //
	// Pre defined sortings that are ready to be used //
	// ---------------------------------------------- //

	// sort child elements verticall and center them //

	public static void sort_vertically(List<UI> _elements)
	{
		if (_elements.Count < 1)
			return;

		var offset = new Vector2();

		Container container = (Container)_elements[0].get_parent();
		if (container == null)
			return;

		for (int i = 0; i < _elements.Count; ++i)
		{
			var old_pos = _elements[i].get_local_position();
			var new_pos = old_pos + offset;

			_elements[i].set_position(new_pos);

			if (_elements[i] is Container resizable)
			{
				//center elements
				new_pos.x = (container.size.x - resizable.get_size().x) * 0.5f;
				_elements[i].set_position(new_pos);

				offset.y = resizable.get_size().y;
				resizable.expand_to_fill = false;
			}
			else
				offset.y += 1;
		}
	}

	// sort child elements vertically but shrink container to fit them //

	public static void sort_vertically_and_shrink_container(List<UI> _elements)
	{
		if (_elements.Count < 1)
			return;

		var offset = new Vector2();
		float max_width = 0;

		for (int i = 0; i < _elements.Count; ++i)
		{
			var old_pos = _elements[i].get_local_position();
			var new_pos = old_pos + offset;

			_elements[i].set_position(new_pos);

			if (_elements[i] is Container resizable)
			{
				offset.y = resizable.get_size().y;
				resizable.expand_to_fill = false;
				if (max_width < resizable.get_size().x)
					max_width = resizable.get_size().x;
			}
			else
				offset.y += 1;
		}

		Container container = (Container)_elements[0].get_parent();
		if (container != null)
		{
			container.set_size(new Vector2(max_width, container.get_size().y));
		}
	}

	// sort child elements horizontally //

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
}
