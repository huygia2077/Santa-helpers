using UnityEngine;
using System.IO;
using System.Collections.Generic;

// Class này để load save dữ liệu theo cấu trúc T bất kỳ
// Xem các cấu trúc có thể dùng trong DataPack.cs


// Cách dùng : 
// DataManager<SettingPack> settingPackManager = new DataManager<SettingPack>("settingPack");
// settingPackManager.Load();

// Khi cần save : 
// settingPackManager.Save();

// Khi cần clear : 
// settingPackManager.ClearData();


public class DataManager<T> where T : new()
{
    public string dataFileName;
    public T dataPack;

    public DataManager(string dataFileName){
        this.dataFileName = dataFileName;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(dataPack);

        char[] chars = json.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = (char)(chars[i] ^ Config.privateKey[i % Config.privateKey.Length]);
        }
        
        string encryptedJson = new string(chars);
        File.WriteAllText(Application.persistentDataPath + dataFileName, encryptedJson);
        Debug.LogWarning("SAVED");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + dataFileName;
        if (!File.Exists(path))
        {
            Save();
            return;
        }

        string encryptedJson = File.ReadAllText(path);
        char[] chars = encryptedJson.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = (char)(chars[i] ^ Config.privateKey[i % Config.privateKey.Length]);
        }

        string json = new string(chars);
        // Thay JsonUtility bằng JsonConvert
        T data = JsonUtility.FromJson<T>(json);


        if (data != null)
        {
            dataPack = data;
        }
    }
    public void ClearData()
    {
        dataPack = new T();
        Save();
    }
}