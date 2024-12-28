using System.Linq;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTools.Localization
{
    [CustomEditor(typeof(LocalizationGameObject))]
    public class LocalizationGameObjectInspector : LocalizationSceneComponentInspectorBase<GameObject>
    {
        protected override string ValueTitle => "GameObject";

        protected override VisualElement MakeValueCell()
        {
            return new ObjectField() { objectType = typeof(GameObject) };
        }

        protected override void BindValueCell(VisualElement ve, int index)
        {
            var gameObjectField = ve as ObjectField;
            var keyValuePair = KVP.ElementAt(index);

            gameObjectField.value = keyValuePair.Value;

            gameObjectField.RegisterValueChangedCallback(evt =>
            {
                KVP[keyValuePair.Key] = (GameObject)evt.newValue;
            });
        }
    }
}