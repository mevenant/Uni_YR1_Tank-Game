using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
interface Collider
{
	Vector2 get_position();
	void set_position(Vector2 _pos);
	bool overlaps(Vector2 _other_position);
	bool overlaps(Collider _other);
}

// -- // -- // -- // -- //
//    CIRCLE COLLIDER   //
// -- // -- // -- // -- //

struct CircleCollider : Collider
{
	Vector2 position;
	public float radius;

	public CircleCollider(float _radius)
	{
		radius = _radius;
		position = new Vector2();
	}

	public void set_radius(float _radius)
	{
		radius = _radius;
	}

	public void set_position(Vector2 _pos)
	{
		position = _pos;
	}

	public Vector2 get_position()
	{
		return position;
	}

	public bool overlaps(Collider _other)
	{
		bool result = false;

		if (_other is CircleCollider _other_circle)
		{
			Vector2 difference = _other_circle.get_position() - position;
			result = difference.Magnitude() * 0.5f < radius;
		}

		return result;
	}

	public bool overlaps(Vector2 _other_position)
	{
		bool result = false;
		Vector2 difference = _other_position - position;
		result = difference.Magnitude() * 0.5f < radius;
		
		return result;
	}
}

// -- // -- // -- // -- //
//     BOX COLLIDER     //
// -- // -- // -- // -- //

struct BoxCollider : Collider
{
	Vector2 position;
	public Vector2 min, max;

	public BoxCollider(Vector2 _min, Vector2 _max)
	{
		min = _min;
		max = _max;
		position = new Vector2();
	}

	public void set_bbox(Vector2 _start, Vector2 _end)
	{
		min = _start;
		max = _end;
	}

	public void set_position(Vector2 _pos)
	{
		position = _pos;
	}

	public Vector2 get_position()
	{
		return position;
	}

	public bool overlaps(Collider _other)
	{
		bool result = false;

		if (_other is BoxCollider _other_box)
		{
			Vector2 min1 = min + position;
			Vector2 max1 = max + position;
			Vector2 min2 = _other_box.min + _other.get_position();
			Vector2 max2 = _other_box.max + _other.get_position();

			result = max2.x >= min1.x && max2.y >= min1.y && min2.x <= max1.x && min2.y <= max1.y;
		}

		return result;
	}

	public bool overlaps(Vector2 _other_position)
	{
		bool result = false;

		
		Vector2 min1 = min + position;
		Vector2 max1 = max + position;
		Vector2 min2 = _other_position;
		Vector2 max2 = _other_position;

		result = max2.x >= min1.x && max2.y >= min1.y && min2.x <= max1.x && min2.y <= max1.y;
		

		return result;
	}
}