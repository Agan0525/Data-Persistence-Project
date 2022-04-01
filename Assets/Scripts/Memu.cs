using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Memu : MonoBehaviour
{
    public static Memu Instance;
    public int UserBestScore;
    public bool isLoad=false;
    public string UserName = "nano";
    [System.Serializable]
    class SaveData
    {
        public int UserBestScore;
        public string UserName;
    }

    public Text User;
    public Text Score;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Memu.Instance.isLoad)
        {
            Debug.Log("1111");
            User.text = Memu.Instance.UserName;
            Score.text = $"Best Score : {Memu.Instance.UserName} : {Memu.Instance.UserBestScore}";
        }
    }
    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.UserName = Memu.Instance.UserName;
        data.UserBestScore = UserBestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            UserName = data.UserName;
            UserBestScore = data.UserBestScore;
            isLoad = true;
        }
    }

    public void StartGame()
    {
        if (User.text != Memu.Instance.UserName)
        {
            Memu.Instance.UserName = User.text;
        }
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Memu.Instance.SaveInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
