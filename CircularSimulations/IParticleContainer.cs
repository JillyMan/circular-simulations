namespace CircularFluid
{
	public interface IParticleContainer
	{
		int Used { get; }

		Particle[] Particles { get; }

		void Add(int x, int y);

		void Update();
	}
}
