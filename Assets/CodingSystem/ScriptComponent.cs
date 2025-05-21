using System.Collections.Generic;
using UnityEngine;

public class ScriptComponent : MonoBehaviour {
    
    private static Dictionary<string, Script> scripts = new();

    public static void Register(string className, Script script) {
        scripts.Add(className, script);
    }
    
    public static void Register((string className, Script script) tuple) {
        if (scripts.ContainsKey(tuple.className)) {
            scripts.Remove(tuple.className);
        }
        scripts.Add(tuple.className, tuple.script);
    }

    public static Script GetScript(string className) {
        if (scripts.ContainsKey(className)) {
            return scripts[className];
        }
        return null;
    }

}
