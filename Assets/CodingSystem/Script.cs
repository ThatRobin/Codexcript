using System;
using System.Collections.Generic;
using System.Linq;
using RoslynCSharp;
using UnityEngine;

public class Script {

    private ScriptType scriptType;
    private Dictionary<string, Method> methods;
    
    public Script(ScriptType scriptType, Dictionary<string, Method> methods) {
        this.scriptType = scriptType;
        this.methods = methods;
    }

    public void Execute(string methodName, ref object[] parameters) {
        if (methods.TryGetValue(methodName, out Method method)) {
            method.Run(scriptType, ref parameters);
        }
    }

    public class Method {
        private string methodName;
        private List<Type> expectedParameters;

        public Method(string methodName, List<Type> expectedParameters) {
            this.methodName = methodName;
            this.expectedParameters = expectedParameters;
        }
        
        public void Run(ScriptType scriptType, ref object[] parameters) {
            if (CSharpInterpreter.ValidateTypes(expectedParameters, parameters.ToList())) {
                scriptType.CallStatic(methodName, ref parameters);
            } else {
                Debug.Log("Failed to find base method to inject into, arguments are incorrectly defined.");
            }
        }
    }
    
}
