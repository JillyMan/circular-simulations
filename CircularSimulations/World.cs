using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CircularFluid
{
	public class World
	{
		private readonly int W;
		private readonly int H;

		private int[] world;
		private int idsUsed;

		public IList<IParticleContainer> ParticleContainers { get; private set; }

		public World(int w, int h, int sandSize)
		{
			world = new int[w * h];
			W = w;
			H = h;

			ParticleContainers = new List<IParticleContainer>
			{
				new SandParticleContainer(this, sandSize)
			};
		}

		public void Update()
		{
			foreach(var container in ParticleContainers)
			{
				container.Update();
			}
		}

		public int Reserve(int x, int y)
		{
			int pos = GetPos(x, y);

			if (pos < 0 || pos > world.Length) return 0; // throw error!

			if (world[pos] == 0)
			{
				world[pos] = ++idsUsed;
			}

			return world[pos];
		}

		public bool TryMove(int x0, int y0, int x1, int y1)
		{
			if (!(ValidateCoord(x0, y0) && ValidateCoord(x1, y1)))
			{
				return false;
			}

			int pos = GetPos(x0, y0);
			if (world[pos] != 0)
			{
				int newPos = GetPos(x1, y1);
				world[newPos] = world[pos];
				world[pos] = 0;

				return true;
			}

			return false;
		}

		public int GetCellId(int x, int y) 
		{
			if (ValidateCoord(x, y))
			{
				return world[GetPos(x, y)];
			}

			return 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetPos(int x, int y) => 
			x + y * W;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool ValidateCoord(int x, int y) =>
			x >= 0 && x < W && y >= 0 && y < H;
	}
}
