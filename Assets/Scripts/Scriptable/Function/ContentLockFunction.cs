using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace JsonClass
{
    public partial class ContentLockScriptable // This Class is a functional Class.
    {
        public ContentLock Get(int id)
        {
            return contentLock.Find(x => x.id == id);
        }
    }

    public partial class ContentLock // This Class is a functional Class.
    {
        public ClientEnum.ContentLockType ContentLockType()
        {
            return (ClientEnum.ContentLockType)contentLockType;
        }

        public string OpenLocal()
        {
            return ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(openLocal);
        }

        public string ClickLocal()
        {
            return string.Format(ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(clicklocal),targetValue);
        }
    }

}
