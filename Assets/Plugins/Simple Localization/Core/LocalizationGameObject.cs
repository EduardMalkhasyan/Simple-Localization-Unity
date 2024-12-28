using ProjectTools.Localization.ScriptableObject;
using UnityEngine;

namespace ProjectTools.Localization
{
    public class LocalizationGameObject : LocalizationAbstractSceneComponent<GameObject>
    {
        private void Awake()
        {
            UpdateLocalizationData(LocalizationLanguage.ImmutableValue.CurrentLanguage);
            LocalizationLanguage.OnLanguageChange += OnLanguageChange;
        }

        private void OnDestroy()
        {
            LocalizationLanguage.OnLanguageChange -= OnLanguageChange;
        }

        protected override void UpdateLocalizationData(SystemLanguage newLanguage)
        {
            foreach (var gameObject in KVP.Values)
            {
                gameObject.SetActive(false);
            }

            KVP[newLanguage].SetActive(true);
        }

        protected override void OnLanguageChange(SystemLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }
    }
}
