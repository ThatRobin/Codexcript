using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class AnnotationHelper
{
    private static Dictionary<string, Type> TYPE_MAP = new() {
        { "int", typeof(int) },
        { "float", typeof(float) },
        { "double", typeof(double) },
        { "bool", typeof(bool) },
        { "string", typeof(string) },
        { "char", typeof(char) },
        { "long", typeof(long) },
        { "decimal", typeof(decimal) },
        { "short", typeof(short) }
    };
    
    // Method to extract annotation name and argument types using regex
    public static (string AnnotationName, List<Type> ArgumentTypes) ExtractAnnotationData(string attributeValue) {
        // Define regex pattern to extract the annotation name and arguments inside parentheses
        var pattern = @"([a-zA-Z0-9]+)\((.*?)\)";
        var match = Regex.Match(attributeValue, pattern);

        if (match.Success) {
            string annotationName = match.Groups[1].Value;

            
            string argumentList = match.Groups[2].Value;

            
            var argumentTypes = argumentList.Split(',')
                .Select(arg => arg.Trim())
                .Select(type => {
                    if (TYPE_MAP.TryGetValue(type, out Type t)) {
                        return t;
                    }
                    return typeof(object);
                }) // Convert argument to a Type
                .ToList();

            return (annotationName, argumentTypes);
        }

        // If no match, return default values
        return (string.Empty, new List<Type>());
    }
    
    // Reusable method to check if a given syntax node has the specified attribute
    private static bool HasAttribute(SyntaxList<AttributeListSyntax> attributeLists, string attributeName, out string attributeArgument) {
        foreach (var attributeListSyntax in attributeLists) {
            foreach (var attributeSyntax in attributeListSyntax.Attributes) {
                if (attributeSyntax.Name.ToString() == attributeName ||
                    attributeSyntax.Name.ToString().EndsWith($".{attributeName}"))
                {
                    // Try to extract the value of the first argument (if any)
                    attributeArgument = GetAttributeArgumentValue(attributeSyntax);
                    return true;
                }
            }
        }
        
        attributeArgument = string.Empty;
        return false;
    }

    private static string GetAttributeArgumentValue(AttributeSyntax attributeSyntax) {
        var argumentList = attributeSyntax.ArgumentList;
        if (argumentList != null && argumentList.Arguments.Count > 0) {
            var firstArgument = argumentList.Arguments[0].Expression;
            if (firstArgument is LiteralExpressionSyntax literalExpression) {
                return literalExpression.Token.ValueText;
            }
        }

        return string.Empty;
    }

    public static (string ClassName, string AttributeArgument) FindAnnotatedClasses(string sourceCode,
            string attributeName) {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = syntaxTree.GetRoot();

        var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

        foreach (var classDeclaration in classDeclarations) {
            if (HasAttribute(classDeclaration.AttributeLists, attributeName, out string attributeArgument)) {
                return (classDeclaration.Identifier.Text, attributeArgument);
            }
        }

        return (string.Empty, string.Empty);
    }
    
    public static List<(string MethodName, string attributeArgument, List<Type> ParameterTypes)> FindAnnotatedMethods(
            string sourceCode, string attributeName) {
        List<(string MethodName, string attributeArgument, List<Type> ParameterTypes)> annotatedMethods = new();
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = syntaxTree.GetRoot();

        var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

        foreach (var methodDeclaration in methodDeclarations) {
            if (HasAttribute(methodDeclaration.AttributeLists, attributeName, out string attributeArgument)) {
                var (annotationName, argumentTypes) = ExtractAnnotationData(attributeArgument);
                
                annotatedMethods.Add((methodDeclaration.Identifier.Text, annotationName, argumentTypes));
            }
        }

        return annotatedMethods;
    }
}