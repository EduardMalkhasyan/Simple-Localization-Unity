using ProjectTools.Localization.ScriptableObject;
using UnityEngine;

namespace ProjectTools.Localization
{
    public abstract class LocalizationAbstractSceneComponent : MonoBehaviour
    {
        protected abstract void UpdateLocalizationData(SystemLanguage newLanguage);
        protected abstract void OnLanguageChange(SystemLanguage newLanguage);
    }
}
