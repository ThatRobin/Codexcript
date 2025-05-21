using System;
using TMPro;
using UnityEngine;

public class CompileCode : MonoBehaviour {
    public TMP_InputField codeText;
    
    public void Run() {
        (string, Script) script = CSharpInterpreter.CompileCSharpCode(codeText.text);
        ScriptComponent.Register(script);
    }
}
