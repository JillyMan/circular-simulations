using System.Collections.Generic;

namespace CircularFluid
{
	public abstract class BaseParticleContainer : IParticleContainer
	{
		protected World world;

		public Particle[] Particles { get; private set; }

		private Dictionary<int, int> idsMap = new Dictionary<int, int>();

		protected BaseParticleContainer(World world, int count)
		{
			this.world = world;
			Particles = new Particle[count];
		}

		public int Used { get; private set; }

		public void Add(int x, int y)
		{
			var id = world.GetCellId(x, y);
			if (id == 0)
			{
				var idInMap = world.Reserve(x, y);

				var internalId = Used++;
				idsMap[idInMap] = internalId;
				ref var p = ref Particles[internalId];
				p.X = x;
				p.Y = y;
				p.Type = ParticleType.Sand;
			}
		}

		public void Update()
		{
			for (int i = 0; i < Used; i++)
			{
				UpdateRule(ref Particles[i]);
			}
		}

		protected abstract void UpdateRule(ref Particle particle);
	}
}
