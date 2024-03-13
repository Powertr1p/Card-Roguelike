using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveLoadSystem : ISaveLoadDataService
    {
        public bool SaveData<T>(string relativePath, T data, bool encrypted)
        {
            string path = Application.persistentDataPath + relativePath;

            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                
                return true;
            }
            catch (Exception e)
            {
                Debug.Log($"Unable to save due: {e.Message} {e.StackTrace}");

                return false;
            }
        }

        public T LoadData<T>(string relativePath, bool ecnrypted)
        {
            throw new NullReferenceException();
        }
    }
}