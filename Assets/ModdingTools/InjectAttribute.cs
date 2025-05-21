using UnityEngine;

using System;

namespace ModdingTools {
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute {
        public string TargetMethod { get; }

        public InjectAttribute(string targetMethod) {
            TargetMethod = targetMethod;
        }
    }
}