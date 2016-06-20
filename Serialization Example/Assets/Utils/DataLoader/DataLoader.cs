using FullSerializer;
using UnityEngine;

namespace DataLoading
{
    /// <summary>
    ///     Can store objects as json and load json as objects
    /// </summary>
    public class DataLoader
    {
        private IDataIO dataIO;

        public DataLoader()
        {
#if UNITY_EDITOR
            dataIO = new DataIOEditor();
#else
            dataIO = new DataIOPlayerPrefs();
#endif
        }

        public T LoadDataObject<T>(T instance) where T : class
        {
            string name = typeof (T).Name;
            return LoadDataObject(name, instance);
        }

        public T LoadDataObject<T>(string name, T instance) where T : class
        {
            string textData = dataIO.ReadData(name);
            fsData data = ParseTextData(textData);

            if (data == null)
                return null;
            fsSerializer serializer = new fsSerializer();

            fsResult result = serializer.TryDeserialize(data, ref instance);
            if (result.Succeeded)
                return instance;
            Debug.LogError(result.FormattedMessages);
            return null;
        }

        private fsData ParseTextData(string textData)
        {
            fsData data = null;
            fsResult parseResult = fsJsonParser.Parse(textData, out data);
            if (parseResult.Succeeded)
                return data;
            Debug.LogError(parseResult.FormattedMessages);
            return null;
        }

        public void StoreDataObject<T>(T instance)
        {
            string name = typeof (T).Name;
            StoreDataObject(name, instance);
        }

        public void StoreDataObject<T>(string name, T instance)
        {
            fsSerializer serializer = new fsSerializer();

            fsData data = null;
            fsResult result = serializer.TrySerialize(instance, out data);
            if (result.Succeeded)
            {
                string stringData = fsJsonPrinter.PrettyJson(data);
                dataIO.WriteData(name, stringData);
            }
            else
                Debug.LogError(result.FormattedMessages);
        }

        public void ClearData<T>(T instance)
        {
            string name = typeof(T).Name;
            dataIO.ClearData(name);
        }
    }
}
