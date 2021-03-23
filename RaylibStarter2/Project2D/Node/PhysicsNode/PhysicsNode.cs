using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

class PhysicsNode : Node
{
	// -- // -- // -- // -- //
	//		  FIELDS
	// -- // -- // -- // -- //
	protected float delta = 0f;

	protected Vector2 velocity;
	protected Vector2 direction;
	protected float speed = 0f;
	protected float max_speed;
	protected float acceleration;
	protected float friction;

	//physics matrices
	protected Matrix3 oriantation_matrix = new Matrix3(true);
	protected Matrix3 translation_matrix = new Matrix3(true);

	public const float ACCELERATION_FAST = 400f;
	public const float ACCELERATION_MED = 200f;
	public const float ACCELERATION_SLOW = 80f;

	public const float FRICTION_HIGH = 100f;
	public const float FRICTION_MED = 50f;
	public const float FRICTION_LOW = 10f;

	public const float MAX_SPEED_HIGH = 300;
	public const float MAX_SPEED_MED = 150;
	public const float MAX_SPEED_LOW = 80;

	// -- // -- // -- // -- //
	//		CONSTRUCTOR
	// -- // -- // -- // -- //
	public PhysicsNode(Node _parent) : base(_parent)
	{
		parent = _parent;
		update_physics_variables();
	}
	
	public PhysicsNode(Node _parent, float _acceleration, float _friction, float _max_speed) : base(_parent)
	{
		parent = _parent;
		update_physics_variables(_acceleration, _friction, _max_speed);
	}

	// -- // -- // -- // -- // -- //
	// Called every physics frame //
	// -- // -- // -- // -- // -- //
	public virtual void _physics_update(float _delta)
	{
		//update delta
		delta = _delta;

		// -- apply friction -- //
		if (speed > 0)
			speed -= friction * delta;

		// -- update velocity -- //
		direction.Normalize();
		velocity = direction * speed * delta;

		// -- apply velocity -- //

		//update transformation matrices
			//oriantation_matrix.Reset();					WARNING: NOT RESETING THIS
		translation_matrix.SetPosition(velocity);

			//update local matrix
		local_transform = local_transform * oriantation_matrix * translation_matrix;

		foreach(Node node in get_children())
		{
			if (node is PhysicsNode physicsNode)
				physicsNode._physics_update(_delta);
		}
	}

	// -- // -- // -- // -- //
	//		 METHODS		//
	// -- // -- // -- // -- //

	//update velocity's direction (NOT VELOCITY ITSELF) and accelerate
	public virtual void _move(Vector2 _input_direction)
	{
		direction = _input_direction;

		if (speed < max_speed)
			speed += acceleration * delta;
	}

	//set acceleration, friction, and max speed
	protected void update_physics_variables(float _acceleration = ACCELERATION_MED, float _friction = FRICTION_MED, float _max_speed = MAX_SPEED_MED)
	{
		acceleration = _acceleration;
		friction = _friction;
		max_speed = _max_speed;
	}
}

