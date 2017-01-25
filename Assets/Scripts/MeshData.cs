﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxelscape
{
    public class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();
        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public MeshData()
        {
        }

        // Takes the last four added vertices and creates two triangles
        // to make a quad with those vertices
        public void AddQuadTriangles()
        {
            // triangle one
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            // triangle two
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
        }
    }
}