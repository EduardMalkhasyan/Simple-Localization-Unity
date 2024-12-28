using ProjectTools.Localization.ScriptableObject;
using TMPro;
using UnityEngine;

namespace ProjectTools.Localization
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class LocalizationTMPDropDown : LocalizationAbstractSceneComponent<string[]>
    {
        [SerializeReference] private TMP_Dropdown dropdown;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();

            UpdateLocalizationData(LocalizationLanguage.ImmutableValue.CurrentLanguage);
            LocalizationLanguage.OnLanguageChange += OnLanguageChange;
        }

        private void OnDestroy()
        {
            LocalizationLanguage.OnLanguageChange -= OnLanguageChange;
        }

        protected override void UpdateLocalizationData(SystemLanguage newLanguage)
        {
            var localizationTexts = KVP[newLanguage];

            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (dropdown.captionText.text == dropdown.options[i].text)
                {
                    dropdown.captionText.text = localizationTexts[i];
                }

                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData
                {
                    image = null,
                    text = localizationTexts[i],
                };

                dropdown.options[i] = optionData;
            }
        }

        protected override void OnLanguageChange(SystemLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }
    }
}
