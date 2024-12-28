using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace ProjectTools.Localization
{
    [CustomEditor(typeof(LocalizationTMPText))]
    public class LocalizationTMPTextInspector : LocalizationSceneComponentInspectorBase<string>
    {
        protected override string ValueTitle => "Text";

        protected override VisualElement MakeValueCell()
        {
            return new TextField();
        }

        protected override void BindValueCell(VisualElement ve, int index)
        {
            var textField = ve as TextField;
            var keyValuePair = KVP.ElementAt(index);

            textField.value = keyValuePair.Value;

            textField.RegisterValueChangedCallback(evt =>
            {
                KVP[keyValuePair.Key] = evt.newValue;
            });
        }
    }
}