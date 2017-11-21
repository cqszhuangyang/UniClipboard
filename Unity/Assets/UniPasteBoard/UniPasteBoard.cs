using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class UniPasteBoard {

	public static string GetClipBoardString() {
#if UNITY_EDITOR
        return UnityEditor.EditorGUIUtility.systemCopyBuffer;
#elif UNITY_IOS
		return _getClipBoardString();
#elif UNITY_ANDROID
		return androidGetClipBoardString();
#elif UNITY_STANDALONE_OSX
		return _getClipBoardString();
#elif UNITY_STANDALONE_WIN
        return UniPasteBoardPlugin.UniPasteBoard.GetClipBoardString();
#elif UNITY_WP8
        Debug.LogWarning("Get clip board content is forbidden in WP8. Returining empty string");
        return"";
#else
		return "";
#endif
    }

	public static void SetClipBoardString(string text) {
#if UNITY_EDITOR
		UnityEditor.EditorGUIUtility.systemCopyBuffer = text;
#elif UNITY_IOS
		_setClipBoardString(text);
#elif UNITY_ANDROID
		androidSetClipBoardString(text);
#elif UNITY_STANDALONE_OSX
		_setClipBoardString(text);
#elif UNITY_STANDALONE_WIN
        UniPasteBoardPlugin.UniPasteBoard.SetClipBoardString(text);
#elif UNITY_WP8
        UniPasteBoardPlugin.UniPasteBoard.SetClipBoardString(text);
#else

#endif
    }
	

#if UNITY_ANDROID && !UNITY_EDITOR
	private static AndroidJavaClass _javaClass;
	private static AndroidJavaClass JavaClass {
		get {
			if (_javaClass == null) {
				try {
					_javaClass = new AndroidJavaClass("com.onevcat.UniPasteBoard.PasteBoard");
				} catch (System.Exception ex) {
					Debug.Log(ex.ToString());
				}
			}
			return _javaClass;
		}
	}

	private static string androidGetClipBoardString() {
		string result = null;
		if (JavaClass != null) {
			result = JavaClass.CallStatic<string>("getClipBoardString");
		}
		return result;
	}

	private static void androidSetClipBoardString(string text) {
		if (JavaClass != null) {
			JavaClass.CallStatic("setClipBoardString", text);
		}
	}
#endif

#if ((UNITY_IOS || UNITY_IPHONE) && !UNITY_EDITOR)
	[DllImport("__Internal")]
	private static extern string _getClipBoardString();
	[DllImport("__Internal")]
	private static extern void _setClipBoardString(string value);
#endif

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
	[DllImport("UniPasteBoard")]
	private static extern string _getClipBoardString();
	[DllImport("UniPasteBoard")]
	private static extern void _setClipBoardString(string value);
#endif

}