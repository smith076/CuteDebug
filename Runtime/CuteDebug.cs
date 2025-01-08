using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;

public static class CuteDebug
{
    private const string USER_PREF_KEY = "CuteDebug_User";

    #region User Management
    private static string CurrentUser
    {
#if UNITY_EDITOR
        get => EditorPrefs.GetString(USER_PREF_KEY, CloudProjectSettings.userName);
#else
        get => "";
#endif
    }

    private static bool ShouldShowDebug(string authorName, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
    {
        if (string.IsNullOrEmpty(authorName))
        {
            // Check for attribute on the calling method
            var stackTrace = new System.Diagnostics.StackTrace();
            var method = stackTrace.GetFrame(2)?.GetMethod();
            var attributes = method?.GetCustomAttributes(typeof(CuteDebugAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                var attr = (CuteDebugAttribute)attributes[0];
                authorName = attr.AuthorName;
            }
        }

#if UNITY_EDITOR
        return string.IsNullOrEmpty(authorName) || authorName == CurrentUser;
#else
        return false;
#endif
    }
    #endregion

    #region Log Overloads
    public static void Log(string message, GameObject gameObject = null, string authorName = null)
    {
#if DEBUG_SYMBOL
        if (message != null && ShouldShowDebug(authorName))
        {
            string formattedMessage = authorName != null ? $"[{authorName}] {message}" : message;

            if (gameObject == null)
                Debug.Log(formattedMessage);
            else
                Debug.Log(formattedMessage, gameObject);
        }
#endif
    }

    public static void Log(GameObject gameObject, string authorName = null)
    {
#if DEBUG_SYMBOL
        if (gameObject != null && ShouldShowDebug(authorName))
        {
            string message = authorName != null ? $"[{authorName}] {gameObject.name}" : gameObject.name;
            Debug.Log(message, gameObject);
        }
#endif
    }

    public static void Log(List<Object> ts, string authorName = null)
    {
#if DEBUG_SYMBOL
        if (ts != null && ts.Count > 0 && ShouldShowDebug(authorName))
        {
            for (int i = 0; i < ts.Count; i++)
            {
                string message = authorName != null ? $"[{authorName}] {ts[i]}" : ts[i].ToString();
                Debug.Log(message);
            }
        }
#endif
    }
    #endregion

    public static void Warning(string message, string authorName = null)
    {
#if DEBUG_SYMBOL
        if (ShouldShowDebug(authorName))
        {
            string formattedMessage = authorName != null ? $"[{authorName}] {message}" : message;
            Debug.LogWarning(formattedMessage);
        }
#endif
    }

    public static void Error(string message, string authorName = null)
    {
#if DEBUG_SYMBOL
        if (ShouldShowDebug(authorName))
        {
            string formattedMessage = authorName != null ? $"[{authorName}] {message}" : message;
            Debug.LogError(formattedMessage);
        }
#endif
    }

    public static void Assert(bool condition, string message = "", string authorName = null)
    {
#if DEBUG_SYMBOL
        if (ShouldShowDebug(authorName))
        {
            string formattedMessage = authorName != null ? $"[{authorName}] {message}" : message;
            Debug.Assert(condition, formattedMessage);
        }
#endif
    }
}

[System.AttributeUsage(System.AttributeTargets.Method)]
public class CuteDebugAttribute : System.Attribute
{
    public string AuthorName { get; private set; }

    public CuteDebugAttribute(string authorName)
    {
        AuthorName = authorName;
    }
}