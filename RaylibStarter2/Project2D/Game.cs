using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;
		public static CollisionManager collision_manager = new CollisionManager();

        private float deltaTime = 0.005f;
		Node root;

        public Game()
        {
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

			add_game_nodes();
		}

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

			//update
			foreach (Node node in root.get_children())
			{
				if (node is PhysicsNode physics_node) 
				{ 
					//update transformation
					physics_node._physics_update(deltaTime);

					//physics
					collision_manager.run();
				}

				node.update_global_transform();
			}
		}

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(RLColor.BLACK);

			//Draw game objects here
            DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);

			//Drawing occures here
			root._draw();

			EndDrawing();
        }


		private void add_game_nodes()
		{
			//init root
			root = new Node(null);

			//Wall
			var wall = new PhysicsNode(root);
			wall.set_texture(Graphics.get_texture_from_path(Graphics.wall));
			wall.set_position(new Vector2(214, 96));

			//Tank
			var tank = new Tank(root);
			tank.set_position(new Vector2(100, 100));

			collision_manager.add_node(tank);
			collision_manager.add_node(wall);
		}

    }
}
