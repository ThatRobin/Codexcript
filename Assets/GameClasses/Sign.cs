using System;
using UnityEngine;

public class Sign : GameScript, IInteractable {
    public DSDialogueSO dialogue;
    public GameObject interactableElement;
    private static Sign instance;

    public void Start() {
        if(instance == null) { instance = this; }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            interactableElement.SetActive(true);
        }
    }

    private void Update() {
        if (GameData.isConversing) {
            interactableElement.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            interactableElement.SetActive(false);
        }
    }

    public void Interact() {
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
