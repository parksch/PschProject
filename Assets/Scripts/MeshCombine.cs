using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{
    private void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        Vector4[] lightmapScaleOffsets = new Vector4[renderers.Length];
        int[] lightmapIndices = new int[renderers.Length];
        int vertexCount = 0;

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // Save lightmap data
            lightmapScaleOffsets[i] = renderers[i].lightmapScaleOffset;
            lightmapIndices[i] = renderers[i].lightmapIndex;

            meshFilters[i].gameObject.SetActive(false);

            vertexCount += meshFilters[i].sharedMesh.vertexCount;
        }

        MeshFilter parentMeshFilter = GetComponent<MeshFilter>();
        MeshRenderer parentMeshRenderer = GetComponent<MeshRenderer>();

        if (parentMeshFilter == null)
        {
            parentMeshFilter = gameObject.AddComponent<MeshFilter>();
        }

        if (parentMeshRenderer == null)
        {
            parentMeshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        parentMeshFilter.mesh = new Mesh();

        if (vertexCount > 65535)
        {
            parentMeshFilter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        }

        parentMeshFilter.mesh.CombineMeshes(combine);

        parentMeshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        parentMeshRenderer.lightmapIndex = lightmapIndices[0];
        parentMeshRenderer.lightmapScaleOffset = Vector4.zero; 

        gameObject.SetActive(true);
    }
}
