using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class IDETextEditor : MonoBehaviour {

    public TMP_InputField text;
    public TMP_Text displayText;
    public SerializableDictionary<string, Color> colourCodes = new SerializableDictionary<string, Color>();

    public TMP_Text lineCounterText;
    public TMP_Text codeText;


    public void OnValueChanged(String value)
    {
        String newValue = value;
        Debug.Log(newValue);

        foreach (var item in colourCodes.keys) {
            newValue = newValue.Replace(item.key, $"<color=#{ColorUtility.ToHtmlStringRGB(item.value)}>{item.key}</color>");
        }

        displayText.text = newValue;
        Debug.Log(newValue);
        
        /*
         * lineCounterText.text = "";

        for (int i = 0; i < text.text.Split("\n").Length; i++)
        {
            lineCounterText.text += $"{i+1}\n";
        }
        */

        //lineCounterText.rectTransform.rect.yMin = 1; = new Vector3(0, 15 + (15 * (codeText.text.Split('\n').Length-1)), 0);
    }
}


[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<Entry> keys = new List<Entry>();
    
    [Serializable]
    public class Entry
    {
        public TKey key;
        public TValue value;
    }

    public Entry GetEntry(int index)
    {
        return keys[index];
    }

    public TKey getKey(int index)
    {
        return keys[index].key;
    }

    public TValue getValue(int index)
    {
        return keys[index].value;
    }
}
