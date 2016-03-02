using UnityEngine;

namespace DataLoading
{
    /// <summary>
    ///     Stores data in the player prefs
    /// </summary>
    public class DataIOPlayerPrefs : IDataIO
    {
        public string ReadData(string name)
        {
            return PlayerPrefs.GetString(name, null);
        }

        public void WriteData(string name, string data)
        {
            PlayerPrefs.SetString(name, data);
        }

        public void ClearData(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }
    }
}