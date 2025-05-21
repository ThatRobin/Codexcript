using System;
using System.Collections.Generic;
using RoslynCSharp;

public static class CSharpInterpreter  {
    private static ScriptDomain domain = null;

    public static (string, Script) CompileCSharpCode(string sourceCode) {
        if (domain == null) {
            domain = ScriptDomain.CreateDomain("Modding Domain");
        }
        
        ScriptAssembly assembly = domain.CompileAndLoadSource(sourceCode);

        if (assembly == null) return (String.Empty, null);
        
        (string, string) classData = AnnotationHelper.FindAnnotatedClasses(sourceCode, "Injector");
        Dictionary<string, Script.Method> methods = new Dictionary<string, Script.Method>();
        if (classData.Item1 != null) {
            ScriptType clazz = assembly.FindType(classData.Item1);
            if (clazz != null) {
                List<(string, string, List<Type>)> methodNames = AnnotationHelper.FindAnnotatedMethods(sourceCode, "Inject");
                foreach (var methodName in methodNames) {
                    Script.Method method = new Script.Method(methodName.Item1, methodName.Item3);
                    methods.Add(methodName.Item2, method);
                }
            }
            Script script = new Script(clazz, methods);
            return (classData.Item2, script);
        }

        return (String.Empty, null);
    }
    
    public static bool ValidateTypes(List<Type> expectedTypes, List<object> objects) {
        if (expectedTypes.Count != objects.Count) {
            return false;
        }

        for (int i = 0; i < objects.Count; i++) {
            if (objects[i] == null) {
                if (expectedTypes[i].IsValueType && Nullable.GetUnderlyingType(expectedTypes[i]) == null) {
                    return false;
                }
            }
            else if (expectedTypes[i] != objects[i].GetType()) {
                return false;
            }
        }
        return true;
    }
}
