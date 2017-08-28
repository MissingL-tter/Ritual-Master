using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelDataEditor : EditorWindow {

	public LevelData levelData;

	private string levelDataFilePath = "/StreamingAssets/LevelData.json";

	[MenuItem("Window/Level Data Editor")]
	static void Init () {

		LevelDataEditor window = (LevelDataEditor) EditorWindow.GetWindow(typeof(LevelDataEditor));
		window.Show();

	}

	void OnGUI () {

		if (levelData != null) {

			SerializedObject serializedObject = new SerializedObject(this);
			SerializedProperty serializedProperty = serializedObject.FindProperty("levelData");

			EditorGUILayout.PropertyField(serializedProperty, true);

			serializedObject.ApplyModifiedProperties();

			if (GUILayout.Button("Save Level Data")) {
				SaveLevelData();
			}
		}

		if (GUILayout.Button("Load Level Data")) {
			LoadLevelData();
		}

	}

	private void LoadLevelData () {
		string filePath = Application.dataPath + levelDataFilePath;

		if (File.Exists(filePath)) {
			string levelDataAsJson = File.ReadAllText(filePath);
			levelData = JsonUtility.FromJson<LevelData>(levelDataAsJson);
		} else {
			Debug.Log("No Level Data Found");
		}
	}

	private void SaveLevelData () {
		string filePath = Application.dataPath + levelDataFilePath;
		string levelDataAsJson = JsonUtility.ToJson(levelData);
		File.WriteAllText (filePath, levelDataAsJson);
	}
	
}
