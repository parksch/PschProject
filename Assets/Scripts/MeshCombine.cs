using UnityEngine;

public class MeshCombine : MonoBehaviour
{
    private void Start()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];
        MeshRenderer[] meshRenderers = new MeshRenderer[meshFilters.Length];

        Vector4[] lightmapScaleOffsets = new Vector4[meshFilters.Length];
        int lightmapIndex = -1; 

        Matrix4x4 parentInverseMatrix = transform.worldToLocalMatrix;

        for (int i = 0; i < meshFilters.Length; i++)
        {
            MeshFilter meshFilter = meshFilters[i];
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                combineInstances[i].mesh = meshFilter.sharedMesh;
                combineInstances[i].transform = parentInverseMatrix * meshRenderer.transform.localToWorldMatrix;


                lightmapScaleOffsets[i] = meshRenderer.lightmapScaleOffset;

                if (lightmapIndex == -1)
                {
                    lightmapIndex = meshRenderer.lightmapIndex; 
                }
                else if (lightmapIndex != meshRenderer.lightmapIndex)
                {
                    Debug.LogWarning("?ºÏù¥?∏Îßµ ?∏Îç±?§Í? ?ºÏπò?òÏ? ?äÏäµ?àÎã§. ?òÎÇò???ºÏù¥?∏Îßµ?ºÎ°ú ?µÏùº?òÏÑ∏??");
                }
            }

            meshFilters[i].gameObject.SetActive(false);
        }


        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 
        };

        combinedMesh.CombineMeshes(combineInstances, true, true);

        MeshFilter parentMeshFilter = GetComponent<MeshFilter>();
        if (parentMeshFilter == null)
        {
            parentMeshFilter = gameObject.AddComponent<MeshFilter>();
        }

        MeshRenderer parentMeshRenderer = GetComponent<MeshRenderer>();
        if (parentMeshRenderer == null)
        {
            parentMeshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        parentMeshFilter.mesh = combinedMesh;
        parentMeshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        parentMeshRenderer.lightmapIndex = lightmapIndex; 

        Vector2[] originalUVs = combinedMesh.uv2;
        Vector2[] adjustedUVs = new Vector2[originalUVs.Length];

        int vertexOffset = 0;
        for (int i = 0; i < meshFilters.Length; i++)
        {
            Mesh mesh = meshFilters[i].sharedMesh;
            Vector4 scaleOffset = lightmapScaleOffsets[i];

            for (int j = 0; j < mesh.vertexCount; j++)
            {
                Vector2 uv = originalUVs[vertexOffset + j];
                adjustedUVs[vertexOffset + j] = new Vector2(
                    uv.x * scaleOffset.x + scaleOffset.z, 
                    uv.y * scaleOffset.y + scaleOffset.w  
                );
            }

            vertexOffset += mesh.vertexCount;
        }

        combinedMesh.uv2 = adjustedUVs; 
        gameObject.SetActive(true);
    }
}
