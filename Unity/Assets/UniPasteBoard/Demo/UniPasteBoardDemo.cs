using UnityEngine;
using System.Collections;

public class UniPasteBoardDemo : MonoBehaviour {
    string s = "Input here...";
	int screenWidth;
	void Start () {
		screenWidth = Screen.width;
	}
	
	void OnGUI() {
		s = GUILayout.TextArea(s, GUILayout.Width(screenWidth), GUILayout.MinHeight(200));

		if (GUILayout.Button("Copy to System paste board",GUILayout.Height(80.0f))) {
			UniPasteBoard.SetClipBoardString(s);	
		}

		if (GUILayout.Button("Paste From System paste board",GUILayout.Height(80.0f))) {
			s = UniPasteBoard.GetClipBoardString();
		}
	}
}
