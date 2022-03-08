using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropInto : MonoBehaviour, IDropHandler {
    [SerializeField] private Canvas canvas;
    private RectTransform rect;
    public RectTransform bottom;
    public RectTransform top;
    public RectTransform middle;
    public Vector2 nextPos;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        var offset = new Vector2(-rect.rect.width / 2f, rect.rect.height / 2f);
        // var topLeft = rect.anchoredPosition + offset;
        var topLeft = Vector2.zero;
        nextPos = topLeft + new Vector2(middle.rect.width, -top.rect.height);
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("On drop");
        if (eventData.pointerDrag != null) {
            // var offsetY = new Vector2(0, -10);
            eventData.pointerDrag.transform.SetParent(transform, false);
            var etrans = eventData.pointerDrag.GetComponent<RectTransform>();
            var blockOffset = new Vector2(etrans.rect.width / 2f, -etrans.rect.height / 2f);
            etrans.anchoredPosition = nextPos + blockOffset;
            shiftDown(etrans.rect.height, etrans);
        }
    }
    public void moveDown(float y) {
        bottom.anchoredPosition += Vector2.down * y;
        middle.sizeDelta += Vector2.up * y;
        middle.anchoredPosition += Vector2.down * y / 2f;
        // rect.sizeDelta += Vector2.up * y;
        // rect.anchoredPosition += Vector2.down * y / 2f;
        nextPos += Vector2.down * y;
    }

    public void moveUp(float y) {
        bottom.anchoredPosition += Vector2.up * y;
        middle.sizeDelta += Vector2.down * y;
        middle.anchoredPosition += Vector2.up * y / 2f;
        // rect.sizeDelta += Vector2.down * y;
        // rect.anchoredPosition += Vector2.up * y / 2f;
        nextPos += Vector2.up * y;
    }

    public void shiftUp(float y, RectTransform removed) {
        moveUp(y);
        bool test = false;
        foreach (RectTransform child in transform) {
            if (test) {
                if (child.TryGetComponent(out Draggable drag)) {
                    child.anchoredPosition += Vector2.up * y;
                }
            }
            if (removed == child) {
                test = true;
            }
        }
        if (transform.parent.TryGetComponent(out DropInto drop)) {
            drop.shiftUp(y, transform as RectTransform);
        }
    }


    public void shiftDown(float y, RectTransform added) {
        moveDown(y);
        bool test = false;
        foreach (RectTransform child in transform) {
            if (test) {
                if (child.TryGetComponent(out Draggable drag)) {
                    child.anchoredPosition += Vector2.down * y;
                }
            }
            if (added == child) {
                test = true;
            }
        }
        if (transform.parent.TryGetComponent(out DropInto drop)) {
            drop.shiftDown(y, transform as RectTransform);
        }
    }

    // TODO
    // if draggable is close enough show outline either in or under
    // and make sure outline is either element or wrapper template
    // add a get height function when removing and adding
}