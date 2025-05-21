using System;
using System.IO;
using UnityEngine;

public class CodeManager : MonoBehaviour {
    public static CodeManager Instance { get; private set; }
    
    public void Start() {
        Instance = this;
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "test.cs");
        if (File.Exists(filePath)) {
            string fileContents = File.ReadAllText(filePath);
            
            (string, Script) script = CSharpInterpreter.CompileCSharpCode(fileContents);
            ScriptComponent.Register(script);
            
        } else {
            Debug.Log($"File 'test.cs' not found in {currentDirectory}");
        }
    }
}