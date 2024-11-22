# Simple-Localization-Unity

Has extensions only with TextMesh Pro not with Unity text. Aslo this plugin is recomended for small projects! if you want for medium and + size projects use this [Version](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/releases/tag/Localization_v_1_0_2_Odin_Support) which is works only with [Odin Inspector](https://odininspector.com/)

## Download Unity Package:
[Download](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/releases)

## How to Use:
Simply import the Unity Package, and it's ready to use! The plugin works seamlessly with ([Serialiable Dictionary](https://github.com/EduardMalkhasyan/Serializable-Dictionary-Unity)) optimizing link passes.

Script example - For further details, refer to the demo scene within the plugin
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
## Possibilities of this plugin:

1 TMP Text Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/fd721716-96a4-4dd2-9bed-6343fd4cdb93)

2 TMP Dropdown Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/2c19e574-3b36-4dc7-bd23-28fa823629ec)

3 GameObject Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/15b5426c-9910-432d-8d5c-b1646655bd87)

4 Internal (from script) Localization 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/126983e1-c449-430d-8422-2ad491e7262c)

## Plugin control location 

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/232e7e28-d10b-4882-b5a6-975103b509af)


## Final Result:

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/1c0dcfda-e40a-44c2-a029-b89b8048505d)

![image](https://github.com/EduardMalkhasyan/Simple-Localization-Unity/assets/78969017/f4f8d14f-fb0c-4eb9-a2bf-a373ee700c79)

