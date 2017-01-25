using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxelscape
{
    public class Block
    {
        // Equal to 1 divided by number of tiles per side, defaults to 4 tiles per side.
        protected const float TileSize = 0.25f;

        public Block()
        {
        }

        public enum Direction
        {
            North,
            East,
            South,
            West,
            Up,
            Down
        }

        public struct Tile
        {
            public int X;
            public int Y;
        }

        public virtual Tile TexturePosition(Direction direction)
        {
            var tile = new Tile
            {
                X = 0,
                Y = 0
            };
            return tile;
        }

        public virtual Vector2[] FaceUVs(Direction direction)
        {
            Vector2[] UVs = new Vector2[4];
            Tile tilePos = TexturePosition(direction);
            UVs[0] = new Vector2(TileSize * tilePos.X + TileSize,
                TileSize * tilePos.Y);
            UVs[1] = new Vector2(TileSize * tilePos.X + TileSize,
                TileSize * tilePos.Y + TileSize);
            UVs[2] = new Vector2(TileSize * tilePos.X,
                TileSize * tilePos.Y + TileSize);
            UVs[3] = new Vector2(TileSize * tilePos.X,
                TileSize * tilePos.Y);
            return UVs;
        }

        // Each block is solid on a face-by-face basis
        public virtual bool IsSolid(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return true;
                case Direction.East:
                    return true;
                case Direction.South:
                    return true;
                case Direction.West:
                    return true;
                case Direction.Up:
                    return true;
                case Direction.Down:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        public virtual MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.useRenderDataForCol = true;
            if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.Down))
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }
            if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.Up))
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }
            if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.South))
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }
            if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.North))
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }
            if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.West))
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }
            if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.East))
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }
            return meshData;
        }

        protected MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.Up));
            return meshData;
        }

        protected MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.Down));
            return meshData;
        }

        protected MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.South));
            return meshData;
        }

        protected MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.North));
            return meshData;
        }

        protected MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.West));
            return meshData;
        }

        protected MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            meshData.uv.AddRange(FaceUVs(Direction.East));
            return meshData;
        }
    }
}