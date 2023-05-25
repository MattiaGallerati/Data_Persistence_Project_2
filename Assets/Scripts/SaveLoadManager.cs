using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    public int highScore = 0;
    public string highScoreName = string.Empty;
    public bool newHighScore;
    public bool isGameover = false;

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScoreName;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }
    public void Save()
    {
        SaveData data = new SaveData();
        data.HighScore = highScore;
        data.HighScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.HighScore;
            highScoreName = data.HighScoreName;
        }
    }
}
