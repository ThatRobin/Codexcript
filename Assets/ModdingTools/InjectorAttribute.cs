using UnityEngine;

using System;

namespace ModdingTools {
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectorAttribute : Attribute {
        public string TargetClass { get; }

        public InjectorAttribute(string targetClass) {
            TargetClass = targetClass;
        }
    }
}