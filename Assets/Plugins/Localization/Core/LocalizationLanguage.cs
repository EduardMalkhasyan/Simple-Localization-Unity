using ProjectTools.Tools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ProjectTools.Localization.ScriptableObject
{
    public enum GameLanguage
    {
        English,
        Russian
    }

    public class LocalizationLanguage : SOLoader<LocalizationLanguage>
    {
        private const string currentLanguageKey = "LocalizationLanguage_CurrentLanguageKey";
        public static event System.Action<GameLanguage> OnLanguageChange;

        [SerializeField] private bool canShowDebugMessage;

        public GameLanguage CurrentLanguage
        {
            get
            {
                if (DataSaver.TryLoadAsJSON(out GameLanguage value, customKey: currentLanguageKey, showDebug: false))
                {
                    return value;
                }
                else
                {
                    DataSaver.SaveAsJSON(GameLanguage.English, customKey: currentLanguageKey, showDebug: false);
                    return GameLanguage.English;
                }
            }
            private set
            {
                DataSaver.SaveAsJSON(value, customKey: currentLanguageKey, showDebug: canShowDebugMessage);
                OnLanguageChange?.Invoke(value);
            }
        }

        public void SetLanguage(GameLanguage newLanguage)
        {
            CurrentLanguage = newLanguage;
        }

        public void ResetData()
        {
            CurrentLanguage = GameLanguage.English;
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
            component.SetLanguage((GameLanguage)EditorGUILayout.EnumPopup("Current Language", component.CurrentLanguage));
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
