namespace Voxelscape
{
    namespace Blocks
    {
        // Represents an Air block
        public class BlockAir : Block
        {
            public BlockAir() : base()
            {
            }

            public override MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
            {
                return meshData;
            }

            public override bool IsSolid(Block.Direction direction)
            {
                return false;
            }
        }
    }
}