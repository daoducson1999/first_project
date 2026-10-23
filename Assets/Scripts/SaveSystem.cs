using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = GetSavePath();

        File.WriteAllText(path, json);
        Debug.Log("Game Saved");
    }

    public SaveData LoadGame()
    {
        string path = GetSavePath();
        if(!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public bool HasSavedFile()
    {
        return File.Exists(GetSavePath());
    }

    public void DeleteSave()
    {
        string path = GetSavePath();

        if(File.Exists(path)) File.Delete(path);
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "savegame.json");
    }
}
