using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Characters/PlayerDialogue")]
public class PlayerDialogue : Dialogue {

    public SerializedConversation[] options;


    public Dictionary<string, Conversation> getOptions() {
        Dictionary<string, Conversation> list = new Dictionary<string, Conversation>();
        foreach (SerializedConversation entry in options) {
            list.Add(entry.text, entry.conversation);
        }
        return list;
    }

    [Serializable]
    public class SerializedConversation {
        public string text;
        public Conversation conversation;
    }

}
