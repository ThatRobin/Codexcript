using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IDragHandler, IPointerDownHandler {
    public RectTransform windowRect;
    public RectTransform taskBarRect;

    public Vector2 minimumSize = new Vector2(100, 100);
    
    private Vector2 initialMousePosition;
    private Vector2 initialSizeDelta;
    private Vector2 initialAnchoredPosition;

    private enum DragDirection { None, Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight, Move }
    private DragDirection dragDirection = DragDirection.None;

    private Canvas canvas;
    [SerializeField] private float edgeThreshold = 7.5f;
    [SerializeField] private float barEdgeThreshold = 22.5f;

    private bool isMaximised = false;

    private Vector2 cachedAnchorPos;
    private Vector2 cachedSizeDelta;

    public GameObject windowTitle;
    
    void Awake() {
        canvas = gameObject.GetComponentInParent<Canvas>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        DesktopInteraction.GetInstance().Select(this.gameObject);
        
        transform.SetAsLastSibling();
        // Store the initial state
        initialMousePosition = Input.mousePosition;
        initialSizeDelta = windowRect.sizeDelta;
        initialAnchoredPosition = windowRect.anchoredPosition;

        // Determine which edge or corner the user clicked on
        dragDirection = GetDragDirection(eventData.position);
    }

    public void SetTaskBar(RectTransform taskBar) {
        taskBarRect = taskBar;
    }
    
    public void SetTitle(string title) {
        windowTitle.GetComponent<TMP_Text>().text = title;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (dragDirection == DragDirection.None)
            return;

        Vector2 mouseDelta = ((Vector2)Input.mousePosition - initialMousePosition) / canvas.scaleFactor;

        // Update the windowRect based on the drag direction
        switch (dragDirection) {
            case DragDirection.Left:
                ResizeLeft(mouseDelta.x);
                break;
            case DragDirection.Right:
                ResizeRight(mouseDelta.x);
                break;
            case DragDirection.Top:
                ResizeTop(mouseDelta.y);
                break;
            case DragDirection.Bottom:
                ResizeBottom(mouseDelta.y);
                break;
            case DragDirection.TopLeft:
                ResizeLeft(mouseDelta.x);
                ResizeTop(mouseDelta.y);
                break;
            case DragDirection.TopRight:
                ResizeRight(mouseDelta.x);
                ResizeTop(mouseDelta.y);
                break;
            case DragDirection.BottomLeft:
                ResizeLeft(mouseDelta.x);
                ResizeBottom(mouseDelta.y);
                break;
            case DragDirection.BottomRight:
                ResizeRight(mouseDelta.x);
                ResizeBottom(mouseDelta.y);
                break;
            case DragDirection.Move:
                MoveWindow(mouseDelta);
                break;
        }
    }

    private DragDirection GetDragDirection(Vector2 pointerPosition) {
        Vector2 localMousePosition = windowRect.InverseTransformPoint(pointerPosition);
        bool onLeft = localMousePosition.x <= -windowRect.rect.width / 2 + edgeThreshold;
        bool onRight = localMousePosition.x >= windowRect.rect.width / 2 - edgeThreshold;
        bool onTop = localMousePosition.y >= windowRect.rect.height / 2 - edgeThreshold;
        bool onBottom = localMousePosition.y <= -windowRect.rect.height / 2 + edgeThreshold;
        bool onBar = localMousePosition.y >= windowRect.rect.height / 2 - barEdgeThreshold;
        if (onLeft && onTop) return DragDirection.TopLeft;
        if (onRight && onTop) return DragDirection.TopRight;
        if (onLeft && onBottom) return DragDirection.BottomLeft;
        if (onRight && onBottom) return DragDirection.BottomRight;
        if (onLeft) return DragDirection.Left;
        if (onRight) return DragDirection.Right;
        if (onTop) return DragDirection.Top;
        if (onBottom) return DragDirection.Bottom;
        if(onBar) return DragDirection.Move;

        return DragDirection.None;
    }
    
    private void ResizeLeft(float delta)
    {
        float newWidth = Mathf.Max(initialSizeDelta.x - delta, minimumSize.x);
        float widthDelta = initialSizeDelta.x - newWidth;

        windowRect.sizeDelta = new Vector2(newWidth, windowRect.sizeDelta.y);
        windowRect.anchoredPosition = new Vector2(initialAnchoredPosition.x + widthDelta / 2, windowRect.anchoredPosition.y);
    }

    private void ResizeRight(float delta)
    {
        float newWidth = Mathf.Max(initialSizeDelta.x + delta, minimumSize.x);
        float widthDelta = newWidth - initialSizeDelta.x;

        windowRect.sizeDelta = new Vector2(newWidth, windowRect.sizeDelta.y);
        windowRect.anchoredPosition = new Vector2(initialAnchoredPosition.x + widthDelta / 2, windowRect.anchoredPosition.y);
    }

    private void ResizeTop(float delta)
    {
        float newHeight = Mathf.Max(initialSizeDelta.y + delta, minimumSize.y);
        float heightDelta = newHeight - initialSizeDelta.y;

        windowRect.sizeDelta = new Vector2(windowRect.sizeDelta.x, newHeight);
        windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, initialAnchoredPosition.y + heightDelta / 2);
    }

    private void ResizeBottom(float delta)
    {
        float newHeight = Mathf.Max(initialSizeDelta.y - delta, minimumSize.y);
        float heightDelta = initialSizeDelta.y - newHeight;

        windowRect.sizeDelta = new Vector2(windowRect.sizeDelta.x, newHeight);
        windowRect.anchoredPosition = new Vector2(windowRect.anchoredPosition.x, initialAnchoredPosition.y + heightDelta / 2);
    }
    
    private void MoveWindow(Vector2 delta)
    {
        windowRect.anchoredPosition = initialAnchoredPosition + delta;
    }

    public void Maximise() {
        if (!isMaximised) {
            cachedAnchorPos = windowRect.anchoredPosition;
            cachedSizeDelta = windowRect.sizeDelta;
            
            windowRect.anchoredPosition = new (0, taskBarRect.sizeDelta.y / 2);
            windowRect.sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta - new Vector2(0, taskBarRect.sizeDelta.y);
            isMaximised = true;
        }
        else {
            windowRect.anchoredPosition = cachedAnchorPos;
            windowRect.sizeDelta = cachedSizeDelta;
            isMaximised = false;
        }
    }
    
    public void Minimise() {
        this.gameObject.SetActive(false);
    }
    
    public void Close() {
        Destroy(this.gameObject);
    }
}