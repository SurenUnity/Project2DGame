using System;
using UnityEngine;

namespace Utils
{
    public static class Logger
    {

        private static bool InfoLogEnabled = true;

        public static void Disable()
        {
            InfoLogEnabled = false;
        }

        public static void Enable()
        {
            InfoLogEnabled = true;
        }
        
        public static void Log(string message)
        {
#if !RELEASE_BUILD || UNITY_EDITOR
            if (InfoLogEnabled)
                Debug.Log(message);
#endif
        }
        public static void Log(object message)
        {
            
#if !RELEASE_BUILD || UNITY_EDITOR
            if (InfoLogEnabled)
                Debug.Log(message);
#endif
            
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
        
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}