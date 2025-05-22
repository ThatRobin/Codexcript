using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInteraction : MonoBehaviour {

    private static GameObject lastSelectedObject;
    private static GameObject selectedObject;
    public GameObject iconParent;
    private Coroutine deselectCoroutine;
    public float decayDelay = 1f;
    public bool updatePlayerOverride;
    public static DesktopInteraction instance;

    void Start() {
        if (instance == null) {
            instance = this;
        }
    }

    public static DesktopInteraction GetInstance() {
        return instance;
    }
    
    public void Select(GameObject gameObject) {
        DesktopIcon desktopIcon = null;
        if (selectedObject != gameObject) {
            if (deselectCoroutine != null) {
                StopCoroutine(deselectCoroutine);
            }
            selectedObject = gameObject;
            Debug.Log("Selected " + selectedObject.name);
            if (gameObject != null) {
                deselectCoroutine = StartCoroutine(DeselectAfterDelay(decayDelay));
            }
        } else if (selectedObject != null && selectedObject.TryGetComponent(out desktopIcon)) {
            desktopIcon.OnOpenSelected();
        }

        if (selectedObject != null && selectedObject.TryGetComponent(out desktopIcon)) {
            desktopIcon.OnSelected();
        }

        lastSelectedObject = selectedObject;
    }
    
    private IEnumerator DeselectAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        selectedObject = null;
        deselectCoroutine = null;
    }

    public static GameObject GetSelectedObject() {
        return selectedObject;
    }

    public bool IsSelectedWindowGame() {
        return (lastSelectedObject != null && lastSelectedObject.name == "GameWindow(Clone)" && !GameData.isConversing) || updatePlayerOverride;
    }
    
    public static GameObject GetLastSelectedObject() {
        return lastSelectedObject;
    }
    
}
