# Simple-Localization-Unity

Has extensions only with Text mesh pro not with Unity text

## Download Unity Package:
[Download](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/releases)

## How to Use:
Just simpy import Unity Package and it ready to use! plugin works with ([Serialiable Dictionary](https://github.com/EduardMalkhasyan/Serializable-Dictionary-Unity)) so its optimazed in link pass

Script example - for more detail look in demo scene inside plugin
```csharp
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
```
## Posibilities of this plugin:

1 TMP Text Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/fd721716-96a4-4dd2-9bed-6343fd4cdb93)

2 TMP Dropdown Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/2c19e574-3b36-4dc7-bd23-28fa823629ec)

3 Gameobject Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/15b5426c-9910-432d-8d5c-b1646655bd87)

3 Internal (from script) Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/126983e1-c449-430d-8422-2ad491e7262c)


## Final Result:

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/1c0dcfda-e40a-44c2-a029-b89b8048505d)

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/f4f8d14f-fb0c-4eb9-a2bf-a373ee700c79)

