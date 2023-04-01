using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralAsteroid : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private static int _displacementId = Shader.PropertyToID("_Displacement");
    private struct SquareFace
    {
        public Vector3 Origin;
        public Vector3 UDirection;
        public Vector3 VDirection;
    }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        var rand = new Random();
        var mesh = new Mesh
        {
            name = "Quad"
        };
        var resolution = 10;
        var faces = new SquareFace[]
        {
            new SquareFace()
            {
                Origin = new Vector3(-1.0f, -1.0f, -1.0f),
                UDirection = new Vector3(0.0f, 2.0f, 0.0f),
                VDirection = new Vector3(2.0f, 0.0f, 0.0f)
            },
            new SquareFace()
            {
                Origin = new Vector3(1.0f, -1.0f, -1.0f),
                UDirection = new Vector3(0.0f, 2.0f, 0.0f),
                VDirection = new Vector3(0.0f, 0.0f, 2.0f)
            },
            new SquareFace()
            {
                Origin = new Vector3(1.0f, -1.0f, 1.0f),
                UDirection = new Vector3(0.0f, 2.0f, 0.0f),
                VDirection = new Vector3(-2.0f, 0.0f, 0.0f)
            },
            new SquareFace()
            {
                Origin = new Vector3(-1.0f, -1.0f, 1.0f),
                UDirection = new Vector3(0.0f, 2.0f, 0.0f),
                VDirection = new Vector3(0.0f, 0.0f, -2.0f)
            },
            new SquareFace()
            {
                Origin = new Vector3(1.0f, 1.0f, -1.0f),
                UDirection = new Vector3(-2.0f, 0.0f, 0.0f),
                VDirection = new Vector3(0.0f, 0.0f, 2.0f)
            },
            new SquareFace()
            {
                Origin = new Vector3(1.0f, -1.0f, -1.0f),
                UDirection = new Vector3(0.0f, 0.0f, 2.0f),
                VDirection = new Vector3(-2.0f, 0.0f, 0.0f)
            }
        };
        var vertices = new Vector3[6 * (resolution + 1) * (resolution + 1)];
        var indices = new int[resolution * resolution * 36];
        var _displacement = (float)rand.NextDouble();
        _meshRenderer.material.SetFloat(_displacementId, _displacement);

        // generate the values for the vertices and indices arrays
        for (var face = 0; face < 6; face++)
        {
            var vertexOffset = face * (resolution + 1) * (resolution + 1);
            var indexOffset = face * 6 * resolution * resolution;
            for (var i = 0; i <= resolution; i++)
            {
                for (var j = 0; j <= resolution; j++)
                {
                    Vector3 vertex = faces[face].Origin + faces[face].UDirection * i / resolution + faces[face].VDirection * j / resolution;
                    vertex = Vector3.Normalize(vertex);
                    vertex = vertex * (1.0f + Perlin.Noise(vertex.x, vertex.y, vertex.z) * _displacement);
                    vertices[vertexOffset + i * (resolution + 1) + j] = vertex;
                }
            }
            for (var i = 0; i < resolution; i++)
            {
                for (var j = 0; j < resolution; j++)
                {
                    var beginIndex = 6 * (i * resolution + j) + indexOffset;
                    indices[beginIndex] = i * (resolution + 1) + j + vertexOffset;
                    indices[beginIndex + 1] = (i + 1) * (resolution + 1) + j + vertexOffset;
                    indices[beginIndex + 2] = i * (resolution + 1) + j + 1 + vertexOffset;
                    indices[beginIndex + 3] = indices[beginIndex + 1];
                    indices[beginIndex + 4] = (i + 1) * (resolution + 1) + j + 1 + vertexOffset;
                    indices[beginIndex + 5] = indices[beginIndex + 2];
                }
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = indices;
        _meshFilter.mesh = mesh;
    }
}
