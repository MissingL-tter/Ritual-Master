using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public SoundController soundController;
    public GameObject ritual;

    [Header("Resources")]
    public GameObject[] resourceTypes;

    [Header("Level")]
    public int currentLevel;
    public LevelData levelData;

    private string filePath;

    void Awake () {

        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "LevelData.json");

        if (Application.platform == RuntimePlatform.Android) {
            WWW www = new WWW(filePath);
            while(!www.isDone);
            string levelDataAsJson = www.text;
		    levelData = JsonUtility.FromJson<LevelData>(levelDataAsJson);
        } else {
            string levelDataAsJson = File.ReadAllText(filePath);
		    levelData = JsonUtility.FromJson<LevelData>(levelDataAsJson);
        }

        if (!PlayerPrefs.HasKey("CurrentLevel")) {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    void Start () {
        ritual = GameObject.FindWithTag("Ritual");
    }

    void Update () {

    }

    public void AttemptRitual () {
        int[] results = ritual.GetComponent<RitualManager>().EvaluateGuess();
    }

    public void LoadNextLevel () {
        currentLevel += 1;
        SceneManager.LoadScene("Ritual");
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }

    public GameObject GetResource (int resourceId) {
        if (resourceId >= 0) {
            return resourceTypes[resourceId];
        } else {
            Debug.Log("Tried to get resource of type '-1'");
            return null;
        }
    }

}
