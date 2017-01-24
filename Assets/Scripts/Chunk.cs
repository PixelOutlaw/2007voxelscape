using System.Collections;
using UnityEngine;
using Voxelscape.Blocks;

namespace Voxelscape
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        Block[,,] blocks;
        public static int chunkSize = 16;
        public bool update = true;

        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;

        // Use this for initialization
        public void Start()
        {
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            _meshCollider = gameObject.GetComponent<MeshCollider>();

            blocks = new Block[chunkSize, chunkSize, chunkSize];
            for (var x = 0; x < chunkSize; x++)
            {
                for (var y = 0; y < chunkSize; y++)
                {
                    for (var z = 0; z < chunkSize; z++)
                    {
                        blocks[x, y, z] = new BlockAir();
                    }
                }
            }
            for (var x = 0; x < chunkSize; x++)
            {
                for (var y = 0; y < chunkSize; y++)
                {
                    for (var z = 0; z < chunkSize; z++)
                    {
                        blocks[x, y, z] = new BlockAir();
                    }
                }
            }

            blocks[3, 4, 2] = new Block();
            blocks[2, 4, 3] = new Block();
            blocks[2, 3, 4] = new Block();
            blocks[4, 3, 2] = new Block();
            blocks[4, 2, 3] = new Block();
            blocks[3, 2, 4] = new Block();

            UpdateChunk();

            Camera.main.transform.LookAt(gameObject.transform.position);
        }

        // Update is called once per frame
        public void Update()
        {
        }

        // Gets the block at the given coordinates
        public Block GetBlock(int x, int y, int z)
        {
            return blocks[x, y, z];
        }

        // Updates the chunk based on its contents
        public void UpdateChunk()
        {
            var meshData = new MeshData();
            for (var x = 0; x < chunkSize; x++)
            {
                for (var y = 0; y < chunkSize; y++)
                {
                    for (var z = 0; z < chunkSize; z++)
                    {
                        meshData = blocks[x, y, z].BlockData(this, x, y, z, meshData);
                    }
                }
            }
            RenderMesh(meshData);
        }

        // Sends the calculated mesh infomration
        // to the mesh and collision components
        public void RenderMesh(MeshData meshData)
        {
            _meshFilter.mesh.Clear();
            _meshFilter.mesh.vertices = meshData.vertices.ToArray();
            _meshFilter.mesh.triangles = meshData.triangles.ToArray();
        }
    }
}