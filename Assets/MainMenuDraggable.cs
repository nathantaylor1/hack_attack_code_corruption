using UnityEngine;
using UnityEngine.EventSystems;

// https://www.youtube.com/watch?v=sXTAzcxNqv0
// https://www.youtube.com/watch?v=sXTAzcxNqv0
public class MainMenuDraggable : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    /*public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData) data;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out pos
        );
        
        transform.position = canvas.transform.TransformPoint(pos);
    }*/

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
