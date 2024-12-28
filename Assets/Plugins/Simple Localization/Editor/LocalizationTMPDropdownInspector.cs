using System;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;

namespace ProjectTools.Localization
{
    [CustomEditor(typeof(LocalizationTMPDropDown))]
    public class LocalizationTMPDropdownInspector : LocalizationSceneComponentInspectorBase<string[]>
    {
        private TMP_Dropdown dropdown;

        private int previousOptionsCount;

        protected override string ValueTitle => "Options";

        protected override void Awake()
        {
            base.Awake();

            var target = base.target as LocalizationTMPDropDown;
            dropdown = target.GetComponent<TMP_Dropdown>();

            previousOptionsCount = dropdown.options.Count;

            EditorApplication.update += Update;
        }

        void Update()
        {
            if (previousOptionsCount == dropdown.options.Count) return;

            RefreshTable();

            previousOptionsCount = dropdown.options.Count;
        }

        protected override VisualElement MakeValueCell() => new ListView
        {
            makeNoneElement = () => new Label("No options"),
            makeItem = () =>
            {
                var textField = new TextField();
                textField.style.marginRight = 3;

                return textField;
            }
        };

        protected override void BindValueCell(VisualElement ve1, int i)
        {
            var listView = ve1 as ListView;
            var key = KVP.ElementAt(i).Key;

            KVP[key] ??= new string[dropdown.options.Count];
            string[] values = KVP[key];
            
            if (values.Length != dropdown.options.Count)
            {
                Array.Resize(ref values, dropdown.options.Count);
            }

            listView.bindItem = (ve2, j) =>
            {
                var textField = ve2 as TextField;
                textField.textEdition.placeholder = "Option " + (j + 1);
                textField.value = values[j];

                EventCallback<ChangeEvent<string>> callback = evt => values[j] = evt.newValue;
                textField.RegisterCallback(callback);
                textField.userData = callback;
            };

            listView.unbindItem = (textField, _) =>
            {
                var callback = (EventCallback<ChangeEvent<string>>)textField.userData;
                textField.UnregisterCallback(callback);
            };

            listView.itemsSource = values;
        }
    }
}