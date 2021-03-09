using System;

namespace CircularFluid
{
	// Bug: i see holes in the sand!
	class Program
	{
		static int w = 100;
		static int h = 50;
		static int sandParticlesCount = 1000;
		static bool isRunning;

		static char[] screenBuffer;
		static World world;

		static void Main()
		{
			Console.SetWindowSize(w, h);
			Console.SetBufferSize(w, h);
			Console.CursorVisible = false;

			screenBuffer = new char[w * h]; ;
			world = GenWorld(w, h, sandParticlesCount);
			SimulationLoop();
		}

		static void SimulationLoop()
		{
			isRunning = true;

			var startTime = DateTime.Now;
			var fps = 24f;
			var updatesPerMS = 1_000f / fps;
			var delta = default(TimeSpan);

			while (isRunning)
			{
				var curTime = DateTime.Now;
				delta += curTime - startTime;
				startTime = curTime;

				if (delta.Milliseconds > updatesPerMS)
				{
					delta = default;
					InputProcess();
					world.Update();
					Render();
				}
			}
		}

		static World GenWorld(int w, int h, int sandSize)
		{
			var world = new World(w, h, sandSize);
			var r = new Random();
			for (int i = 0; i < sandSize; ++i)
			{
				int x = r.Next(w);
				int y = r.Next(h);
				world.ParticleContainers[0].Add(x, y);
			}
			
			return world;
		}

		static void Render()
		{
			for (int i = 1; i < world.ParticleContainers[0].Used; i++)
			{
				ref var p = ref world.ParticleContainers[0].Particles[i];
				var c = (char)p.Type;
				screenBuffer[p.X + p.Y * w] = c;
			}

			Console.SetCursorPosition(0, 0);
			Console.Write(screenBuffer);

			Array.Fill(screenBuffer, ' ');
		}

		static void InputProcess()
		{
			if (Console.KeyAvailable)
			{
				var key = Console.ReadKey();
				if (key.Key == ConsoleKey.Escape) isRunning = false;
				if (key.Key == ConsoleKey.R) world = GenWorld(w, h, sandParticlesCount);
			}
		}
	}
}
