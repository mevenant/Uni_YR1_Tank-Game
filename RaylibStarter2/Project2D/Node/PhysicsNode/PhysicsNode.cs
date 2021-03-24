using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;

struct Collider
{
	public float radius;

	public Collider(float _radius)
	{
		radius = _radius;
	}

}
class PhysicsNode : Node
{
	// -- // -- // -- // -- //
	//		  FIELDS
	// -- // -- // -- // -- //
	protected float delta = 0f;

	protected Vector2 velocity;
	protected Vector2 direction;	//the direction vector applied to velocity
	protected float speed = 0f;		//@Warning: speed can be negative
	protected float max_speed;
	protected float acceleration;
	protected float friction;
	public	  Collider collider = new Collider(16f);

	//physics matrices
	protected Matrix3 oriantation_matrix = new Matrix3(true);
	protected Matrix3 translation_matrix = new Matrix3(true);
	protected Matrix3 previous_transform = new Matrix3(true);

	public const float ACCELERATION_FAST = 400f;
	public const float ACCELERATION_MED = 200f;
	public const float ACCELERATION_SLOW = 80f;

	public const float FRICTION_HIGH = 100f;
	public const float FRICTION_MED = 80f;
	public const float FRICTION_LOW = 40f;

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
		
		speed -= friction * delta * Math.Sign(speed);
		speed = clamp(speed, -max_speed, max_speed);
		

		// -- update velocity -- //
		direction.Normalize();
		velocity = direction * speed * delta;

		// -- apply velocity -- //

		//update transformation matrices
			//oriantation_matrix.Reset();					WARNING: NOT RESETING THIS
		translation_matrix.SetPosition(velocity);

			//update local matrix
		local_transform = local_transform * oriantation_matrix * translation_matrix;

		previous_transform = local_transform;

		foreach (Node node in get_children())
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
	public Collider get_collider()
	{
		return collider;
	}

	//set acceleration, friction, and max speed
	protected void update_physics_variables(float _acceleration = ACCELERATION_MED, float _friction = FRICTION_MED, float _max_speed = MAX_SPEED_MED)
	{
		acceleration = _acceleration;
		friction = _friction;
		max_speed = _max_speed;
	}

	public virtual void _on_collision(PhysicsNode _other)
	{
		Console.WriteLine("collision occured");
	}
}

