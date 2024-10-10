using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class LocalizationScriptable
    {
        public List<TextCountry> textCountries = new List<TextCountry>();

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

        public string Get(string name)
        {
            TextCountry country = textCountries.Find(x => x.language == DataManager.Instance.Language);

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
