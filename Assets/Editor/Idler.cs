using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Idler : EditorWindow {
	
	[MenuItem("Window/Idler")]
	public static void ShowWindow() {
		Idler w = (Idler)GetWindow(typeof(Idler));
		w.minSize = new Vector2(1,1);
	}

	void Update() {
		Repaint();
	}
}
