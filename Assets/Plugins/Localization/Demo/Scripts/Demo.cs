using ProjectTools.Localization.ScriptableObject;
using ProjectTools.Tools;
using TMPro;
using UnityEngine;

namespace BugiGames.Main
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown chooseLanguageDropDown;

        [SerializeField] private TextMeshProUGUI internalNameText;
        [SerializeField] private SerializableDictionary<SystemLanguage, string> internalNames;

        private void Awake()
        {
            chooseLanguageDropDown.value = LocalizationLanguage.ImmutableValue.CurrentLanguage == SystemLanguage.English
                                           ? chooseLanguageDropDown.value = 0 : chooseLanguageDropDown.value = 1;

            chooseLanguageDropDown.onValueChanged.AddListener((value) =>
            {
                SystemLanguage language = SystemLanguage.English;

                switch (value)
                {
                    // SystemLanguage.English
                    case 0:
                        language = SystemLanguage.English;
                        break;

                    // SystemLanguage.Russian
                    case 1:
                        language = SystemLanguage.Russian;
                        break;
                }

                LocalizationLanguage.ImmutableValue.SetLanguage(language);
            });

            internalNameText.text = internalNames[LocalizationLanguage.ImmutableValue.CurrentLanguage];
            LocalizationLanguage.OnLanguageChange += UpdateInternalName;
        }

        public void UpdateInternalName(SystemLanguage gameLanguage)
        {
            internalNameText.text = internalNames[gameLanguage];
        }
    }
}
