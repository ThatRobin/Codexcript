using UnityEngine;

public class Interactable : GameScript {
    
    public GameObject interactableElement;
    
    public virtual void Interact() { }
    
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            interactableElement.SetActive(true);
        }
    }
    
    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            interactableElement.SetActive(false);
        }
    }

    
}
