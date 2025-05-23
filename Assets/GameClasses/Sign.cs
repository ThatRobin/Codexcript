using System;
using UnityEngine;

public class Sign : Interactable {
    public DSDialogueSO dialogue;
    private static Sign instance;

    public void Start() {
        if(instance == null) { instance = this; }
    }
    
    private void Update() {
        if (GameData.isConversing) {
            interactableElement.SetActive(false);
        }
    }
    
    public override void Interact() {
        if (!GameData.isConversing) {
            ConversationHandler.GetInstance().startConversation(dialogue);
        }
    }


    public static string SGetText() {
        return instance.GetText();
    }
    
    public string GetText() {
        string text = ConversationHandler.GetInstance().currentDialogue.Text;
        
        Tuple<string> parameters = Tuple.Create(text);
        text = ExecuteMethod("GetText", ref parameters).Item1;
        
        return text;
    }
    
}
