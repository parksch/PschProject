using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageText : UIText
{
    [SerializeField] string id;
    [SerializeField] Animator animator;

    public string ID => id;

    public void SetDamageText(UNBigStats stats)
    {
        text.text = stats.Text;
        gameObject.SetActive(true);
        animator.Play("DamageText", -1, 0);
    }
     
    public void Enquque()
    {
        PoolManager.Instance.Enqueue(ID, gameObject);
    }

}
