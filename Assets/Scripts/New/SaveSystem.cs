using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevel(LevelManager lvl)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(lvl);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /*public static LevelData LoadLevel()
    { 
    
    }*/
}
