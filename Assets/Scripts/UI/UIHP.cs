using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    [SerializeField,ReadOnly] Slider slider;
    [SerializeField,ReadOnly] BaseCharacter target;
    [SerializeField,ReadOnly] string id;

    public string ID => id;
    public void SetHPRatio(float hp) => slider.value = hp;
    
    RectTransform rect;
    Camera cam;

    public void SetHPBar(BaseCharacter character,Camera game)
    {
        rect = GetComponent<RectTransform>();
        target = character;
        slider.value = 1;
        cam = game;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (target != null)
        {
            rect.position = cam.transform.position + (target.transform.position - cam.transform.position).normalized * 7f;
        }
    }

    public void ReleaseHpBar()
    {
        target = null;
        PoolManager.Instance.Enqueue(ID, gameObject);
    }
}
