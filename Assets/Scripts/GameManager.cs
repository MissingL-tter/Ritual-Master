using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public SoundController soundController;

    [Header("Resources")]
    public GameObject[] resourceTypes;

    [Header("Level")]
    public int currentLevel;
    public LevelData levelData;

    // Awake is always called before any Start functions
    void Awake () {

        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("CurrentLevel")) {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }

        string levelDataAsJson = File.ReadAllText(Application.streamingAssetsPath + "/LevelData.json");
		levelData = JsonUtility.FromJson<LevelData>(levelDataAsJson);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

    }

    void Start () {

    }

    void Update () {

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
