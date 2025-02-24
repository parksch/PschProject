using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonPanel : BasePanel
{
    [SerializeField, ReadOnly] Text desc;
    [SerializeField, ReadOnly] GameObject yesObject;
    [SerializeField, ReadOnly] GameObject noObject;
    [SerializeField, ReadOnly] GameObject okObject;

    System.Action yesAction, noAction, okAction;

    public override void OnUpdate()
    {
    }


    public void SetYesNo(string descKey,System.Action no = null, System.Action yes = null)
    {
        yesObject.SetActive(true);
        noObject.SetActive(true);
        yesAction = yes;
        noAction = no;
        desc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(descKey);
    }

    public void SetYesNoTypeText(string text, System.Action no = null, System.Action yes = null)
    {
        yesObject.SetActive(true);
        noObject.SetActive(true);
        yesAction = yes;
        noAction = no;
        desc.text = text;
    }

    public void SetOK(string descKey, System.Action ok = null)
    {
        okObject.SetActive(true);
        okAction = ok;
        desc.text = ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(descKey);
    }

    public void SetOKTypeText(string text, System.Action ok = null)
    {
        okObject.SetActive(true);
        okAction = ok;
        desc.text = text;
    }

    public override void Close()
    {
        yesObject.SetActive(false);
        noObject.SetActive(false);
        okObject.SetActive(false);
        yesAction = null;
        noAction = null;
        okAction = null;
    }

    public void OnClickYes()
    {
        yesAction?.Invoke();
        UIManager.Instance.BackPanel();
    }

    public void OnClickNo()
    {
        noAction?.Invoke();
        UIManager.Instance.BackPanel();
    }

    public void OnClickOk()
    {
        okAction?.Invoke();
        UIManager.Instance.BackPanel();
    }
}
