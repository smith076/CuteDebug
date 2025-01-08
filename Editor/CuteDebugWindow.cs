using UnityEngine;
using UnityEditor;


public class CuteDebugWindow : EditorWindow
{
    private const string USER_PREF_KEY = "CuteDebug_User";
    private string username = "";

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.delayCall += CheckAndShowWindow;
    }

    private static void CheckAndShowWindow()
    {
        string currentUser = EditorPrefs.GetString(USER_PREF_KEY, CloudProjectSettings.userName);
        if (string.IsNullOrEmpty(currentUser))
        {
            ShowWindow();
        }
    }

    [MenuItem("Window/CuteDebug Settings")]
    public static void ShowWindow()
    {
        var window = GetWindow<CuteDebugWindow>();
        window.titleContent = new GUIContent("CuteDebug Settings");
        window.minSize = new Vector2(250, 100);
        window.Show();
    }

    private void OnEnable()
    {
        username = EditorPrefs.GetString(USER_PREF_KEY, CloudProjectSettings.userName);
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("CuteDebug Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        EditorGUI.BeginChangeCheck();
        username = EditorGUILayout.TextField("Username:", username);
        
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetString(USER_PREF_KEY, username);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("This username will be used to filter debug messages. Current Unity Cloud username: " + CloudProjectSettings.userName, MessageType.Info);
    }
}
