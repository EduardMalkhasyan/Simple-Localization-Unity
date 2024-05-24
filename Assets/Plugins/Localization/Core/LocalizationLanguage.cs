using ProjectTools.Tools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ProjectTools.Localization.ScriptableObject
{
    public class LocalizationLanguage : SOLoader<LocalizationLanguage>
    {
        private const string currentLanguageKey = "LocalizationLanguage_CurrentLanguageKey";
        public static event System.Action<SystemLanguage> OnLanguageChange;

        [SerializeField] private bool canShowDebugMessage;

        public SystemLanguage CurrentLanguage
        {
            get
            {
                return (SystemLanguage)PlayerPrefs.GetInt(currentLanguageKey, (int)SystemLanguage.English);
            }
            private set
            {
                PlayerPrefs.SetInt(currentLanguageKey, (int)value);

                if (canShowDebugMessage)
                {
                    Debug.Log($"Current language now is {value}");
                }

                OnLanguageChange?.Invoke(value);
            }
        }

        public void SetLanguage(SystemLanguage newLanguage)
        {
            CurrentLanguage = newLanguage;
        }

        public void ResetData()
        {
            CurrentLanguage = SystemLanguage.English;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LocalizationLanguage))]
    public class LocalizationLanguageEditor : Editor
    {
        private SerializedProperty canShowDebugMessageProp;

        private void OnEnable()
        {
            canShowDebugMessageProp = serializedObject.FindProperty("canShowDebugMessage");
        }

        public override void OnInspectorGUI()
        {
            LocalizationLanguage component = (LocalizationLanguage)target;

            serializedObject.Update();
            component.SetLanguage((SystemLanguage)EditorGUILayout.EnumPopup("Current Language", component.CurrentLanguage));
            EditorGUILayout.PropertyField(canShowDebugMessageProp, new GUIContent("Show Debug Messages"));

            if (GUILayout.Button("Reset Data"))
            {
                component.ResetData();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
