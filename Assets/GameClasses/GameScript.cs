using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameScript : MonoBehaviour {
    
    public string GetClassName() {
        return this.GetType().Name;
    }
    
    public T1 ExecuteMethod<T1>(string methodName, ref T1 parameters) {
        var tupleType = parameters.GetType();
        var itemCount = tupleType.GetProperties().Length;
        object[] array = new object[itemCount];
        Type[] types = new Type[itemCount];
        for (int i = 0; i < itemCount; i++) {
            array[i] = tupleType.GetProperty($"Item{i + 1}")?.GetValue(parameters);
            types[i] = array[i].GetType();
        }
        Script script = ScriptComponent.GetScript(GetClassName());
        if (script != null) {
            script.Execute(methodName, ref array);
        }
        var genericTupleType = typeof(Tuple<>).MakeGenericType(types);
        
        var tupleInstance = Activator.CreateInstance(genericTupleType, array);
        return (T1)tupleInstance;
    }
    
}
