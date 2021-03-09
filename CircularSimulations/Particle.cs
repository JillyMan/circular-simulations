namespace CircularFluid
{
	public struct Particle
	{
		public ParticleType Type;
		public int X, Y;

		public Particle(int x, int y, ParticleType type)
		{
			X = x;
			Y = y;
			Type = type;
		}
	}
}
