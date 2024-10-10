using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class LocalizationScriptable : ScriptableObject
    {
        public List<Localization> localization;
    }

    [System.Serializable]
    public partial class Localization
    {
        public int language;
        public List<Textkeys> textkeys;
    }

    [System.Serializable]
    public partial class Textkeys
    {
        public string name;
        public string desc;
    }

}
