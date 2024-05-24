using Newtonsoft.Json;
using UnityEngine;

namespace ProjectTools.Tools
{
    public static class DataSaver
    {
        public static void SaveAsJSON<TValue>(TValue value, string customKey = null, bool showDebug = true)
        {
            var dataKey = customKey == null ? $"{typeof(TValue)}" : $"{typeof(TValue)}_{customKey}";
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);
            PlayerPrefs.SetString($"{dataKey}", json);

            if (showDebug)
            {
                Debug.Log($"Saved from key: {dataKey}, data: {json}");
            }
        }

        public static TValue LoadAsJSON<TValue>(string customKey = null, bool showDebug = true)
        {
            string data = null;
            var dataKey = customKey == null ? $"{typeof(TValue)}" : $"{typeof(TValue)}_{customKey}";

            if (PlayerPrefs.HasKey(dataKey))
            {
                data = PlayerPrefs.GetString(dataKey);

                if (showDebug)
                {
                    Debug.Log($"Loaded from key: {dataKey}, data: {data}");
                }
            }
            else
            {
                if (showDebug)
                {
                    Debug.LogWarning($"Data of key: {dataKey} dose not exist");
                }
            }

            return JsonConvert.DeserializeObject<TValue>(data);
        }

        public static TValue TryLoadAsJSON<TValue>(out bool isAnyData, string customKey = null, bool showDebug = true)
        {
            string data = null;
            var dataKey = customKey == null ? $"{typeof(TValue)}" : $"{typeof(TValue)}_{customKey}";

            if (PlayerPrefs.HasKey($"{dataKey}"))
            {
                data = PlayerPrefs.GetString($"{dataKey}");

                if (showDebug)
                {
                    Debug.Log($"Loaded from key: {dataKey}, data: {data}");
                }

                isAnyData = true;
                return JsonConvert.DeserializeObject<TValue>(data);
            }
            else
            {
                if (showDebug)
                {
                    Debug.LogWarning($"Data of key: {dataKey} dose not exist");
                }

                isAnyData = false;
                return default;
            }
        }

        public static bool TryLoadAsJSON<TValue>(out TValue value, string customKey = null, bool showDebug = true)
        {
            string data = null;
            var dataKey = customKey == null ? $"{typeof(TValue)}" : $"{typeof(TValue)}_{customKey}";

            if (PlayerPrefs.HasKey($"{dataKey}"))
            {
                data = PlayerPrefs.GetString($"{dataKey}");

                if (showDebug)
                {
                    Debug.Log($"Loaded from key: {dataKey}, data: {data}");
                }

                value = JsonConvert.DeserializeObject<TValue>(data);
                return true;
            }
            else
            {
                if (showDebug)
                {
                    Debug.LogWarning($"Data of key: {dataKey} dose not exist");
                }

                value = default;
                return false;
            }
        }

        public static void DeleteAsJSON<TValue>(string customKey = null, bool showDebug = true)
        {
            var dataKey = customKey == null ? $"{typeof(TValue)}" : $"{typeof(TValue)}_{customKey}";

            if (PlayerPrefs.HasKey(dataKey))
            {
                PlayerPrefs.DeleteKey(dataKey);

                if (showDebug)
                {
                    Debug.Log($"Deleted data of key: {dataKey}");
                }
            }
            else
            {
                if (showDebug)
                {
                    Debug.LogWarning($"Data of key: {dataKey} dose not exist");
                }
            }
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log($"All data deleted");
        }
    }
}
