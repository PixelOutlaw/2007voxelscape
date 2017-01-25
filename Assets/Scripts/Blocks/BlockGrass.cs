namespace Voxelscape
{
    namespace Blocks
    {
        public class BlockGrass : Block
        {
            public BlockGrass() : base()
            {
            }

            public override Block.Tile TexturePosition(Block.Direction direction)
            {
                switch (direction)
                {
                    case Direction.Up:
                        return new Tile()
                        {
                            X = 2,
                            Y = 0
                        };
                    case Direction.Down:
                        return new Tile()
                        {
                            X = 1,
                            Y = 0
                        };
                    default:
                        return new Tile()
                        {
                            X = 3,
                            Y = 0
                        };
                }
            }
        }
    }
}