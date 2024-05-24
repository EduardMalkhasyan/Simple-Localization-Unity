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
        [SerializeField] private SerializableDictionary<GameLanguage, string> internalNames;

        private void Awake()
        {
            chooseLanguageDropDown.value = (int)LocalizationLanguage.ImmutableValue.CurrentLanguage;

            chooseLanguageDropDown.onValueChanged.AddListener((value) =>
            {
                GameLanguage language = (GameLanguage)value;
                LocalizationLanguage.ImmutableValue.SetLanguage(language);
            });

            internalNameText.text = internalNames[LocalizationLanguage.ImmutableValue.CurrentLanguage];
            LocalizationLanguage.OnLanguageChange += UpdateInternalName;
        }

        public void UpdateInternalName(GameLanguage gameLanguage)
        {
            internalNameText.text = internalNames[gameLanguage];
        }
    }
}
