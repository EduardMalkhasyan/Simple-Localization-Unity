using ProjectTools.Localization.ScriptableObject;
using ProjectTools.Tools;
using TMPro;
using UnityEngine;

namespace ProjectTools.Localization
{
    public class LocalizationGameObject : LocalizationAbstractSceneComponent
    {
        [SerializeField] private SerializableDictionary<GameLanguage, GameObject> gameObjectKVP;

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
            foreach (var gameObject in gameObjectKVP.Values)
            {
                gameObject.SetActive(false);
            }

            gameObjectKVP[newLanguage].SetActive(true);
        }

        protected override void OnLanguageChange(GameLanguage newLanguage)
        {
            UpdateLocalizationData(newLanguage);
        }
    }
}
