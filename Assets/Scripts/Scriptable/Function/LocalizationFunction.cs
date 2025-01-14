using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class LocalizationScriptable // This Class is a functional Class.
    {
        ClientEnum.Language current;
        Dictionary<string, Textkeys> keyValuePairs = new Dictionary<string, Textkeys>();

        public string Get(string name)
        {
            if (current != DataManager.Instance.Language || keyValuePairs.Count == 0)
            {
                current = DataManager.Instance.Language;
                CreateDict();
            }

            if (!keyValuePairs.ContainsKey(name))
            {
                return "NoText";
            }

            string str = keyValuePairs[name].desc;
            str = str.Replace("\\n", "\n");

            return str;
        }

        void CreateDict()
        {
            Localization country = localization.Find(x => (ClientEnum.Language)x.language == current);
            keyValuePairs.Clear();

            foreach (var item in country.textkeys)
            {
                keyValuePairs[item.name] = item;
            }
        }
    }

    public partial class Localization
    {
    }

    public partial class Textkeys
    {
    }

}
