using UnityEngine;

public class Item : Sign {
    private static int RelicCount = 0;
    
    public override void Interact() {
        base.Interact();
        DestroyImmediate(gameObject);
        RelicCount++;
    }

    public static bool HasRelic() {
        return RelicCount == 4;
    }
}
