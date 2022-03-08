using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] private Canvas canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void unClip() {
        if (transform.parent.TryGetComponent(out DropInto drop)) {
            drop.shiftUp(rect.rect.height, transform as RectTransform);
            transform.parent = canvas.transform;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // Debug.Log("OnBeginDrag");
        unClip();
        canvasGroup.alpha = .6f;
        // RectTransformUtility.ScreenPointToLocalPointInRectangle (transform.parent.transform as RectTransform, eventData.position, null, out Vector2 localPoint);
        // rect.anchoredPosition = localPoint;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) {
        // Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
    }

}