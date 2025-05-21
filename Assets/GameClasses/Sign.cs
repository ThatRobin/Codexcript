using UnityEngine;

public class Sign : GameScript, IInteractable {
    public DSDialogueSO dialogue;
    
    private static Sign instance;

    public void Start() {
        if(instance == null) { instance = this; }
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
        return ConversationHandler.GetInstance().currentDialogue.Text;
    }
    
}
