using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTools.Localization
{
    public abstract class LocalizationSceneComponentInspectorBase<TValueType> : Editor
    {
        private MultiColumnListView table;

        protected Dictionary<SystemLanguage, TValueType> KVP { get; private set; }

        /// <summary>
        /// The title that the values column header will show.
        /// </summary>
        protected virtual string ValueTitle => "Value";

        protected virtual void Awake()
        {
            SerializedProperty kvpProp = serializedObject.FindPropertyOrFail("kvp");
            KVP = kvpProp.managedReferenceValue as Dictionary<SystemLanguage, TValueType>;
        }

        public override VisualElement CreateInspectorGUI()
        {
            return LocalizationTableFactory();
        }

        protected abstract VisualElement MakeValueCell();

        protected abstract void BindValueCell(VisualElement ve, int index);

        protected void RefreshTable()
        {
            table.itemsSource = KVP.ToList();
            table.RefreshItems();
        }

        private MultiColumnListView LocalizationTableFactory()
        {
            table = new MultiColumnListView
            {
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                showBorder = true,
                showAlternatingRowBackgrounds = AlternatingRowBackground.ContentOnly,
                reorderable = true,
                horizontalScrollingEnabled = true,
                showAddRemoveFooter = true,
                reorderMode = ListViewReorderMode.Animated,

                itemsSource = KVP.ToList(),

                overridingAddButtonBehavior = (_, _) => OverrideAddBtnBehaviour(),
                onRemove = _ => RemoveItem()
            };

            table.RefreshItems();

            table.columns.stretchMode = Columns.StretchMode.GrowAndFill;

            table.columns.Add(ColumnFactory("Language"));
            table.columns.Add(ColumnFactory(ValueTitle));

            Column languagesColumn = table.columns["Language"];
            languagesColumn.makeCell = () => new Label();
            languagesColumn.bindCell = BindLanguageCell;

            Column valuesColumn = table.columns[ValueTitle];
            valuesColumn.makeCell = () =>
            {
                VisualElement cell = MakeValueCell();
                cell.style.marginRight = 3;

                return cell;
            };
            valuesColumn.bindCell = BindValueCell;

            return table;
        }

        private void OverrideAddBtnBehaviour()
        {
            var dropdownMenu = new GenericMenu();

            var allLanguages = Enum.GetValues(typeof(SystemLanguage)).Cast<SystemLanguage>().ToArray();
            var addedLanguages = KVP.Keys;
            var notAddedLanguages = allLanguages.Except(addedLanguages);

            if (notAddedLanguages.Count() > 0)
            {
                foreach (SystemLanguage language in notAddedLanguages)
                {
                    dropdownMenu.AddItem(
                        content: new GUIContent(language.ToString()),
                        on: false,
                        func: () => AddItem(language)
                        );
                }
            }
            else
            {
                table.allowAdd = false;
            }

            dropdownMenu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
        }

        private void AddItem(SystemLanguage language)
        {
            KVP.Add(language, default);
            table.allowRemove = true;
            RefreshTable();
        }

        private void RemoveItem()
        {
            if (KVP.Count > 0)
            {
                KVP.Remove(key: KVP.Last().Key);

                table.allowAdd = true;
                RefreshTable();
            }
            
            if (KVP.Count == 0)
            {
                table.allowRemove = false;
            }
        }

        private Column ColumnFactory(string title) => new()
        {
            name = title,
            title = title,
            stretchable = true,
            sortable = false,
            optional = false
        };

        private void BindLanguageCell(VisualElement ve, int index)
        {
            var label = ve as Label;
            SystemLanguage language = KVP.ElementAt(index).Key;

            label.text = language.ToString();
        }
    }
}