using System.Collections.Generic;
using UnityEditor.Localization.Editor;
using UnityEngine;

namespace JsonClass
{
    public partial class LocalizationScriptable // This Class is a functional Class.
    {
        public string Get(string name)
        {
            Localization country = localization.Find(x => (ClientEnum.Language)x.language == DataManager.Instance.Language);

            if (country.textkeys.Find(x => x.name == name) == null)
            {
                return "NoText";
            }

            return country.textkeys.Find(x => x.name == name).desc;
        }
    }

    public partial class Localization
    {
    }

    public partial class Textkeys
    {
    }

}
