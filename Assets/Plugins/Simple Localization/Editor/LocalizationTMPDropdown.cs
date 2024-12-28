using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTools.Localization
{
    [CustomEditor(typeof(LocalizationTMPDropDown))]
    public class LocalizationTMPDropdownInspector : LocalizationSceneComponentInspectorBase<string[]>
    {
        private TMP_Dropdown dropdown;
        private readonly List<ListView> optionsListViews = new();

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

        protected override VisualElement MakeValueCell()
        {
            var listView = new ListView
            {
                makeNoneElement = () => new Label("No options"),
                makeItem = () =>
                {
                    var textField = new TextField();
                    textField.style.marginRight = 3;

                    return textField;
                }
            };
            optionsListViews.Add(listView);

            return listView;
        }

        protected override void BindValueCell(VisualElement ve1, int i)
        {
            var listView = ve1 as ListView;
            var key = KVP.ElementAt(i).Key;

            KVP[key] ??= new string[dropdown.options.Count];
            string[] values = KVP[key];
            
            if (values.Length != dropdown.options.Count)
            {
                Array.Resize(ref values, dropdown.options.Count + 1);
            }

            listView.itemsSource = values;

            listView.bindItem = (ve2, j) =>
            {
                var textField = ve2 as TextField;
                textField.textEdition.placeholder = "Option " + (j + 1);
                textField.value = values[j];

                textField.RegisterValueChangedCallback(evt =>
                {
                    values[j] = evt.newValue;
                });
            };
        }
    }
}