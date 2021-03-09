namespace CircularFluid
{
	public class SandParticleContainer : BaseParticleContainer
	{
		public SandParticleContainer(World world, int count) 
			: base(world, count)
		{
		}

		protected override void UpdateRule(ref Particle p)
		{
			int x = p.X;
			int y = p.Y;

			if (CanMoveDown(x, y) && world.TryMove(x, y, x, y + 1))
			{
				p.Y = y + 1;
			}
			else if (CanMoveLeftDown(x, y) && world.TryMove(x, y, x - 1, y + 1))
			{
				p.X = x - 1;
				p.Y = y + 1;
			}
			else if (CanMoveRightDown(x, y) && world.TryMove(x, y, x + 1, y + 1))
			{
				p.X = x + 1;
				p.Y = y + 1;
			}
		}

		private bool CanMoveDown(int x, int y) => world.GetCellId(x, y + 1) == 0;

		private bool CanMoveLeftDown(int x, int y) => world.GetCellId(x - 1, y + 1) == 0;

		private bool CanMoveRightDown(int x, int y) => world.GetCellId(x + 1, y + 1) == 0;
	}
}
