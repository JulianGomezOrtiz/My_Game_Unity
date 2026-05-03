using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "savegame.json");

    public static void SaveGame(int llaves, int puntos)
    {
        SaveData data = new SaveData { llaves = llaves, puntos = puntos };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
        Debug.Log("Juego guardado");
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Juego cargado: " + data.puntos + " puntos, " + data.llaves + " llaves");
            return data;
        }
        return new SaveData { llaves = 0, puntos = 0 };
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Partida borrada");
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public int llaves;
        public int puntos;
    }
}
