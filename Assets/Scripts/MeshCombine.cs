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
        int lightmapIndex = -1; // 라이트맵 인덱스 (모든 메시가 같은 라이트맵을 사용할 경우)

        // 1. 개별 메시 정보 수집
        for (int i = 0; i < meshFilters.Length; i++)
        {
            MeshFilter meshFilter = meshFilters[i];
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                combineInstances[i].mesh = meshFilter.sharedMesh;
                combineInstances[i].transform = meshRenderer.transform.localToWorldMatrix;

                // 라이트맵 정보 수집
                lightmapScaleOffsets[i] = meshRenderer.lightmapScaleOffset;

                if (lightmapIndex == -1)
                {
                    lightmapIndex = meshRenderer.lightmapIndex; // 첫 번째 라이트맵 인덱스 저장
                }
                else if (lightmapIndex != meshRenderer.lightmapIndex)
                {
                    Debug.LogWarning("라이트맵 인덱스가 일치하지 않습니다. 하나의 라이트맵으로 통일하세요.");
                }
            }

            meshFilters[i].gameObject.SetActive(false);
        }

        // 2. 새로운 메시 생성
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 // 버텍스 수가 많을 경우 지원
        };

        combinedMesh.CombineMeshes(combineInstances, true, true);

        // 3. 부모 객체에 MeshRenderer 및 MeshFilter 추가
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

        // 4. 라이트맵 데이터 설정
        parentMeshRenderer.lightmapIndex = lightmapIndex; // 라이트맵 인덱스 설정

        // 라이트맵 UV 조정
        Vector2[] originalUVs = combinedMesh.uv2; // UV2는 라이트맵 UV
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
                    uv.x * scaleOffset.x + scaleOffset.z, // UV x 스케일 및 오프셋 적용
                    uv.y * scaleOffset.y + scaleOffset.w  // UV y 스케일 및 오프셋 적용
                );
            }

            vertexOffset += mesh.vertexCount;
        }

        combinedMesh.uv2 = adjustedUVs; // 라이트맵 UV 설정
        gameObject.SetActive(true);
    }
}
