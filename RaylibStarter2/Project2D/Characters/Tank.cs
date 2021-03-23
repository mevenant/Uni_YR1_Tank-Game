using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

class Tank : PhysicsNode
{
	// -- // -- // -- // -- //
	//		CONSTRUCTOR
	// -- // -- // -- // -- //
	public Tank(Node _parent) : base(_parent)
	{
		update_physics_variables(ACCELERATION_MED, FRICTION_HIGH, MAX_SPEED_LOW);

		texture = Graphics.get_texture_from_path(Graphics.tank_body);
	}

	// -- // -- // -- // -- //
	//		 METHODS
	// -- // -- // -- // -- //
	public override void _physics_update(float _delta)
	{
		oriantation_matrix.Reset();
		translation_matrix.Reset();

		//move based on input
		var input = Input.get_primary_input();
		
		//only change direction when input
		if (input != Vector2.ZERO)
			_move(input);

		base._physics_update(_delta);
	}

	public override void _move(Vector2 _input_direction)
	{
		direction = new Vector2(0, _input_direction.y);
		float rotation_angle = _input_direction.x;
		oriantation_matrix.SetRotateZ(rotation_angle * delta);

		if (speed < max_speed)
			speed += acceleration * delta;

	}
}
