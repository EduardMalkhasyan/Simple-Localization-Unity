using ProjectTools.Localization.ScriptableObject;
using ProjectTools.Tools;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ProjectTools.Localization
{
    public class LocalizationTMPDropDown : LocalizationAbstractSceneComponent
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private SerializableDictionary<GameLanguage, string[]> dropdownKVP;

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
            var localizationTexts = dropdownKVP[newLanguage];

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

        protected override void OnLanguageChange(GameLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }

#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(LocalizationTMPDropDown))]
        public class LocalizationTMPDropDownEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                LocalizationTMPDropDown component = (LocalizationTMPDropDown)target;

                EditorGUILayout.HelpBox("Please check if dropdownKVP must be same size with TMP_Dropdown.dropdown",
                                         UnityEditor.MessageType.Warning);

                base.OnInspectorGUI();
            }
        }
#endif
    }
}
