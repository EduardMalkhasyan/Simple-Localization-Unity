using ProjectTools.Localization.ScriptableObject;
using TMPro;
using UnityEngine;

namespace ProjectTools.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationTMPText : LocalizationAbstractSceneComponent<string>
    {
        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();

            UpdateLocalizationData(LocalizationLanguage.ImmutableValue.CurrentLanguage);
            LocalizationLanguage.OnLanguageChange += OnLanguageChange;
        }

        private void OnDestroy()
        {
            LocalizationLanguage.OnLanguageChange -= OnLanguageChange;
        }

        protected override void UpdateLocalizationData(SystemLanguage newLanguage)
        {
            textMeshProUGUI.text = KVP[newLanguage];
        }

        protected override void OnLanguageChange(SystemLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }
    }
}
