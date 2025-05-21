using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DesktopIcon : MonoBehaviour {

    [SerializeField] private RectTransform taskBar;
    [SerializeField] private GameObject selectedHightlight;
    [SerializeField] private GameObject iconText;
    [SerializeField] private UnityEvent onSelected;
    [SerializeField] private UnityEvent onOpenSelected;
    [SerializeField] private string windowTitle;
    void Update() {
        bool isThisSelected = DesktopInteraction.GetLastSelectedObject() != null &&
                              DesktopInteraction.GetLastSelectedObject() == gameObject;
        
        selectedHightlight.SetActive(isThisSelected);
        RectTransform highlightTransform = selectedHightlight.GetComponent<RectTransform>();
        RectTransform iconTransform = iconText.GetComponent<RectTransform>();
        
        iconTransform.sizeDelta = new Vector2(100, isThisSelected ? 34 : 20);
        int textHeight = Mathf.RoundToInt(iconTransform.GetComponent<TMP_Text>().textBounds.size.y);
        highlightTransform.sizeDelta = new Vector2(90, isThisSelected && textHeight == 33 ? 34 : 20);
        
        //highlightTransform.anchoredPosition = new Vector2(highlightTransform.anchoredPosition.x, isThisSelected ? -14 : 0);
    }

    public void OnSelected() {
        onSelected.Invoke();
    }
    
    public void OnOpenSelected() {
        onOpenSelected.Invoke();
    }

    public void CreateWindow(GameObject prefab) {
        var windowInst = Instantiate(prefab, this.GetComponentInParent<Canvas>().transform);
        windowInst.transform.SetSiblingIndex(windowInst.transform.parent.childCount - 2);
        Window window = windowInst.GetComponent<Window>();
        window.SetTaskBar(taskBar);
        window.SetTitle(windowTitle);
    }
}
