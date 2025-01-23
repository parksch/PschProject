using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    [SerializeField,ReadOnly] string hpbarID;
    [SerializeField,ReadOnly] string damageTextID;
    [SerializeField,ReadOnly] Camera gameCamera;
    [SerializeField,ReadOnly] RectTransform hpParent;
    [SerializeField,ReadOnly] RectTransform damageTextParent;

    [SerializeField,ReadOnly] List<UIHP> uIHPs;
    [SerializeField,ReadOnly] List<UIDamageText> uIDamageTexts;

    public UIHP SetHpBar(BaseCharacter target)
    {
        UIHP bar = PoolManager.Instance.Dequeue(ClientEnum.ObjectType.UI, hpbarID).GetComponent<UIHP>();
        RectTransform rect = bar.GetComponent<RectTransform>();

        rect.SetParent(hpParent);
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;

        bar.SetHPBar(target,gameCamera);

        return bar;
    }

    public void SetDamageText(Vector3 pos,UNBigStats stats)
    {
        UIDamageText damageText = PoolManager.Instance.Dequeue(ClientEnum.ObjectType.UI, damageTextID).GetComponent<UIDamageText>();
        RectTransform rect = damageText.GetComponent<RectTransform>();

        rect.SetParent(damageTextParent);
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;

        rect.position = gameCamera.transform.position + (pos - gameCamera.transform.position).normalized * 7f;

        damageText.SetDamageText(stats);
    }

}
