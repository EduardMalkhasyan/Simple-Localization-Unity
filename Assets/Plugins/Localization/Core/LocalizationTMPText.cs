using ProjectTools.Localization.ScriptableObject;
using ProjectTools.Tools;
using TMPro;
using UnityEngine;

namespace ProjectTools.Localization
{
    public class LocalizationTMPText : LocalizationAbstractSceneComponent
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private SerializableDictionary<GameLanguage, string> textKVP;

        private void Awake()
        {
            UpdateLocalizationData(LocalizationLanguage.ImmutableValue.CurrentLanguage);
            LocalizationLanguage.OnLanguageChange += OnLanguageChange;
        }

        private void OnDestroy()
        {
            LocalizationLanguage.OnLanguageChange -= OnLanguageChange;
        }

        protected override void UpdateLocalizationData(GameLanguage newLanguage)
        {
            textMeshProUGUI.text = textKVP[newLanguage];
        }

        protected override void OnLanguageChange(GameLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }
    }
}
