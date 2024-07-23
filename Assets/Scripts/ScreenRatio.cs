using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    [SerializeField] List<RectTransform> notchPanels = new List<RectTransform>();
    [SerializeField] List<PivotRatio> pivotRatioPanels = new List<PivotRatio>();
    [SerializeField] List<RenderTexture> renderTextures = new List<RenderTexture>();

    [System.Serializable]
    public class PivotRatio
    {
        public RectTransform target;
        public RectTransform pivot;
    }

    private void Start()
    {
        SetNotch();
        SetRenderTextureSize();
        SetPivotRatio();
    }

    void SetNotch()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 newAnchorMin = safeArea.position;
        Vector2 newAnchorMax = safeArea.position + safeArea.size;
        newAnchorMin.x /= Screen.width;
        newAnchorMax.x /= Screen.width;
        newAnchorMin.y = 0;
        newAnchorMax.y = 1;

        for (int i = 0; i < notchPanels.Count; i++)
        {
            notchPanels[i].anchorMin = newAnchorMin;
            notchPanels[i].anchorMax = newAnchorMax;
        }
    }

    void SetPivotRatio()
    {
        Vector2 tempVec2 = Vector2.zero;

        for (int i = 0; i < pivotRatioPanels.Count; i++)
        {
            float value = pivotRatioPanels[i].pivot.rect.width > pivotRatioPanels[i].pivot.rect.height ? pivotRatioPanels[i].pivot.rect.width : pivotRatioPanels[i].pivot.rect.height;
            tempVec2.x = value;
            tempVec2.y = value;
            pivotRatioPanels[i].target.sizeDelta = tempVec2;
        }
    }

    public void SetRenderTextureSize()
    {
        float originWidth = Screen.width;

        //switch (option)
        //{
        //    case 1:
        //        originWidth = originWidth * 0.5f;
        //        break;
        //    case 2:
        //        originWidth = originWidth * .75f;
        //        break;
        //    default:
        //        break;
        //}

        for (int i = 0; i < renderTextures.Count; i++)
        {
            renderTextures[i].Release();
            renderTextures[i].width = System.Convert.ToInt16(originWidth);
            renderTextures[i].height = System.Convert.ToInt16(originWidth);
        }
    }
}
