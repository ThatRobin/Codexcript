using UnityEngine;

namespace ModdingTools {
    public class Logger {

        public static void Info(object message) {
            Debug.Log(message);
        }

        public static void Warn(object message) {
            Debug.LogWarning(message);
        }

        public static void Error(object message) {
            Debug.LogError(message);
        }
    }
}