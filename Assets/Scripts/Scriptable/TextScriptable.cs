using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName = "Scriptable/Text")]
public class TextScriptable : BaseScriptable
{
    [SerializeField] List<TextCountry> textCountries = new List<TextCountry>();

    [System.Serializable]
    public class TextCountry
    {
        public ClientEnum.Language language;
        public List<Textkey> textkeys = new List<Textkey>(); 
    }

    [System.Serializable]
    public class Textkey
    {
        public string name;
        public string desc;
    }

    public string GetText(string name)
    {
       return textCountries.Find(x => x.language == DataManager.Instance.Language).textkeys.Find(x => x.name == name).desc;
    }
}
