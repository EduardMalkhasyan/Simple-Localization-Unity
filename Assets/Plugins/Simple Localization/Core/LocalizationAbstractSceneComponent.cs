using ProjectTools.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTools.Localization
{
    public abstract class LocalizationAbstractSceneComponent<TValueType> : MonoBehaviour
    {
        [SerializeReference] private SerializableDictionary<SystemLanguage, TValueType> kvp = new();

        protected IReadOnlyDictionary<SystemLanguage, TValueType> KVP => kvp;

        protected abstract void UpdateLocalizationData(SystemLanguage newLanguage);
        protected abstract void OnLanguageChange(SystemLanguage newLanguage);
    }
}
