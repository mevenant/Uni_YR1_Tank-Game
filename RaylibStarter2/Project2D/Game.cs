using System;
using System.IO;
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
		enum Modes
		{
			Normal,
			Editor,
		}
		Modes mode = Modes.Editor;

		// -- // -- // -- // -- //
		//		  NORMAL		//
		// -- // -- // -- // -- //
		public static CollisionManager collision_manager = new CollisionManager();
		private Camera2D camera;

		// -- // -- // -- // -- //
		//		  EDITOR		//
		// -- // -- // -- // -- //
		private int editor_grid_size = 64;


        private float deltaTime = 0.005f;
		Node root;
		UI ui_root;

        public Game()
        {
			root = new Node(null);
			ui_root = new UI(null);
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

			if (mode == Modes.Normal)
			{
				generate_world("../Worlds/test_world.txt");
				//add_default_nodes();
			} else if (mode == Modes.Editor)
			{
				//test button
				Button btn_test = new Button(ui_root, "", new Vector2(32, 32), RLColor.BLACK, RLColor.WHITE, RLColor.WHITE, 64, 64);
				Button btn_grid = new Button(ui_root, "", new Vector2(0, 96), Graphics.texture_empty_grid);
				btn_test._action = print_hey;
				//btn_grid._action = btn_grid.set_texture(Graphics.texture_wall);	//TODO: CLEAN UP BEFORE DOING ANYTHING
			}
		}

		public void print_hey()
		{
			Console.WriteLine("Hey :)");
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

			if (mode == Modes.Normal)
			{
				//update
				foreach (Node node in root.get_children())
				{
					if (node is PhysicsNode physics_node) 
					{ 
						//update transformation
						physics_node._physics_update(deltaTime);
					}

					//physics
					node.update_global_transform();
				}

				ui_root.update_global_transform();
				ui_root._update_state();

				collision_manager.run();
			} else if (mode == Modes.Editor)
			{
				ui_root.update_global_transform();
				ui_root._update_state();
			}
		}

        public void Draw()
        {
            BeginDrawing();
			ClearBackground(RLColor.BLACK);

			if (mode == Modes.Normal)
			{
				//Draw game objects here
				DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);
				BeginMode2D(camera);
				//Drawing occures here

				EndMode2D();

				root._draw();
			} else if (mode == Modes.Editor)
			{
				draw_editor();
			}
			
			EndDrawing();
        }

		void draw_editor()
		{
			//Draw title
			DrawText("EDITOR", 10, 10, 14, RLColor.RED);

			//Draw UI
			ui_root._draw();

			//Draw world grid
		}


		private void add_default_nodes()
		{
			//Wall
			var wall = new PhysicsNode(root);
			wall.set_texture(Graphics.get_texture_from_path(Graphics.wall));
			wall.set_position(new Vector2(214, 96));

			//Tank
			var tank = new Tank(root);
			tank.set_position(new Vector2(100, 100));

			collision_manager.add_node(wall);
			collision_manager.add_node(tank);
		}

		private void generate_world(string _data_file_path)
		{
			const int	CELL_SIZE = 64;
			const char	CELL_WALL = 'W';
			const char	CELL_TANK = 'T';

			var physics_nodes = new List<PhysicsNode>();

			try
			{
				StreamReader file = new StreamReader(_data_file_path);
				string line;

				int world_width;

				line = file.ReadLine();
				if (line != null)
					world_width = line.Length;  //The width of the world is the length of the first line
				else
				{
					Console.WriteLine("Error: the first line of the file is empty");
					return;
				}

				int current_y = 0;
				while (line != null)
				{
					for (int current_x = 0; current_x < line.Length; ++current_x)
					{
						char current_cell = line[current_x];
						Vector2 current_pos = new Vector2(current_x * CELL_SIZE + CELL_SIZE / 2, current_y * CELL_SIZE + CELL_SIZE / 2);

						if (current_cell == CELL_WALL)
						{
							PhysicsNode node = new PhysicsNode(root);
							node.set_texture(Graphics.texture_wall);
							node.set_position(current_pos);

							//Add to collision manager
							physics_nodes.Add(node);
						}
						if (current_cell == CELL_TANK)
						{
							var tank = new Tank(root);
							tank.set_position(current_pos);
							Console.WriteLine("\n\nTank was created at: \n" + current_pos);
							//Add to collision manager
							physics_nodes.Add(tank);
						}
					}

					++current_y;
					line = file.ReadLine();
				}

				//add nodes to collision_manager
				foreach (PhysicsNode node in physics_nodes)
				{
					collision_manager.add_node(node);
				}

				file.Close();
				file.Dispose();

			} catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			
		}

		public static RLVector2 get_RLVector2(Vector2 _vector)
		{
			RLVector2 result = new RLVector2();
			result.x = _vector.x;
			result.y = _vector.y;
			return result;
		}

    }
}
