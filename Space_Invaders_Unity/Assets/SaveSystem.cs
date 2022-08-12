using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(Stats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stat_file.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        StatsData data = new StatsData(stats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StatsData LoadData()
    {
        string path = Application.persistentDataPath + "/stat_file.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StatsData data = formatter.Deserialize(stream) as StatsData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
