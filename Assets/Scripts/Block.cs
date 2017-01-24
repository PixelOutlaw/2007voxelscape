using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxelscape
{
    public class Block
    {
        public Block()
        {
        }

        public enum Direction
        {
            North, East, South, West, Up, Down
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
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

        protected MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles();
            return meshData;
        }

    }
}
