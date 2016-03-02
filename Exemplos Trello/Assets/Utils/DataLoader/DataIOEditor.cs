using System.IO;
using UnityEngine;

namespace DataLoading
{
#if UNITY_EDITOR
    /// <summary>
    ///     Stores data in a PlayerConfigData folder next to the Assets folder,
    ///     Used for saving stuff in Editor
    /// </summary>
    public class DataIOEditor : IDataIO
    {
        private string unityFolder;
        private readonly string EXTENSION = ".json";

        public DataIOEditor()
        {
            unityFolder = Application.dataPath.Replace("Assets", "PlayerConfigData/");
        }

        public string ReadData(string name)
        {
            if (Directory.Exists(unityFolder))
            {
                string path = GetFullPath(name);
                if(File.Exists(path))
                    return File.ReadAllText(path);
            }
            return null;
        }

        public void WriteData(string name, string data)
        {
            if (!Directory.Exists(unityFolder))
                Directory.CreateDirectory(unityFolder);

            string path = GetFullPath(name);
            File.WriteAllText(path, data);
        }

        public void ClearData(string name)
        {
            string path = GetFullPath(name);
            File.Delete(path);
        }

        private string GetFullPath(string name)
        {
            return string.Format("{0}{1}{2}", unityFolder, name, EXTENSION);
        }
    }
#endif
}