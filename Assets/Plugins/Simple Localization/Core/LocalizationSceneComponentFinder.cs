using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProjectTools.Localization
{
    public class LocalizationSceneComponentFinder : MonoBehaviour
    {
        [SerializeField] private LocalizationAbstractSceneComponent[] localizationAbstractSceneComponents;

        public void FindComponentsInScene()
        {
            localizationAbstractSceneComponents = FindObjectsOfType<LocalizationAbstractSceneComponent>(includeInactive: true);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LocalizationSceneComponentFinder))]
    public class LocalizationSceneComponentFinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LocalizationSceneComponentFinder componentFinder = (LocalizationSceneComponentFinder)target;

            if (GUILayout.Button("Find Components In Scene"))
            {
                componentFinder.FindComponentsInScene();
            }
        }
    }
#endif
}
