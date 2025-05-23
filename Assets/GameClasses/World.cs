using UnityEngine;

public class World : GameScript {

    private bool HasWon = false;
    
    void Update() {
        if ((!GameData.isConversing) && Item.HasRelic() && !HasWon) {
            HasWon = true;
        }
    }
    
}
