using ProjectTools.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace BugiGames.Main
{
    public class SaveTest : MonoBehaviour
    {
        public List<string> files = new List<string>();

        [ContextMenu("Save")]
        public void Save()
        {
            DataSaver.SaveAsJSON(files, "files");
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var a = DataSaver.LoadAsJSON<List<string>>("files");
        }
    }
}
