using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    protected RectTransform rect;
    protected CanvasGroup canvasGroup;
    //protected Image image;
    public Vector3 locationBeforeDrag;
    protected Transform originalParent;
    public bool droppedInto = false;
    [SerializeField]
    protected GameObject glowLayer = null;
    [SerializeField]
    protected CanvasGroup glowGroup = null;
    protected Coroutine glowCoroutine = null;

    protected virtual void Awake() {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //image = GetComponent<Image>();
    }
    private void OnEnable() {
        if (glowGroup != null) {
            glowCoroutine = StartCoroutine(GlowCoroutine());
        }
    }

    public virtual void ToggleGlow(bool isGlowing)
    {
        if (isGlowing)
        {
            if (glowGroup == null)
            {
                GameObject newGlowLayer = Instantiate(glowLayer, transform);
                glowGroup = newGlowLayer.GetComponent<CanvasGroup>();
            }
            glowCoroutine = StartCoroutine(GlowCoroutine());
        }
        else 
        {
            if (glowCoroutine != null) {
                StopCoroutine(glowCoroutine);
            }
            if (glowGroup != null)
            {
                Destroy(glowGroup.gameObject);
                //glowGroup.alpha = 0f;
            }
        }
    }

    protected IEnumerator GlowCoroutine()
    {
        while (true)
        {
            /*Color color = image.color;
            color.a = value * 0.7f;
            image.color = color;*/
            glowGroup.alpha = Mathf.Sin(Time.unscaledTime * Mathf.PI) * 0.08f + 0.3f;
            yield return null;
        }
    }

    public virtual void UnClip() {
        //Debug.Log("Unclipped");
        if (transform.parent.TryGetComponent(out BlockResizer br))
        {
            br.UpdateSize();
        }

        if (TryGetComponent(out Code code))
        {
            //Debug.Log("Has code");
            code.StopExecution();
            //code.StopSecondaryExecution();
        }
        // transform.SetParent(canvas.transform);
        // transform.SetAsLastSibling();
    }

    public virtual void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        ToggleGlow(false);
        UnClip();
        canvasGroup.alpha = .6f;
        locationBeforeDrag = rect.anchoredPosition3D;
        // must remove parent in order to show thing being dragged on top
        originalParent = transform.parent;
        transform.SetParent(EditorController.instance.editor_screen.transform);

        canvasGroup.blocksRaycasts = false;
        string returnType = GetComponent<Code>().ReturnType;
        foreach (var item in getCanvas().GetComponentsInChildren<Parameter>())
        {
            if (!item.CanAcceptInput(returnType)) {
                // item.GetComponent<CanvasGroup>().blocksRaycasts = false;
            } else {
                // Even if the parameter accepts the input type, 
                // if there is a block already in it we want to make it invisible
                // if the thing inside isn't a codewithparameter then make it invisible
                foreach (Transform item2 in item.transform)
                {
                    if (item2.TryGetComponent(out Code c) 
                        && !item2.TryGetComponent(out CodeWithParameters p)) {
                        item2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                }
            }
        }
    }

    public virtual void OnDrag(PointerEventData eventData) {
        // Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / EditorController.instance.editor_screen.scaleFactor;
        //rect.anchoredPosition = eventData.position / EditorController.instance.editor_screen.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        // Debug.Log(rect.localPosition);
        if ( !droppedInto && !transform.parent.TryGetComponent<Code>(out Code testCode) 
                && !transform.parent.TryGetComponent<InputField>(out InputField testField)) {
            transform.SetParent(originalParent);
            
            // Debug.Log(rect.localPosition);
            RectTransform canvasRect = getCanvas().transform as RectTransform;
            var rectMiddle = rect.localPosition + Vector3.down * rect.rect.height / 2 + Vector3.right * rect.rect.width / 2;
            if (rectMiddle.y > canvasRect.rect.yMax || rectMiddle.y < canvasRect.rect.yMin ||
                rectMiddle.x > canvasRect.rect.xMax || rectMiddle.x < canvasRect.rect.xMin)
            {
                rect.anchoredPosition3D = locationBeforeDrag;
            }
            if (originalParent.TryGetComponent(out BlockResizer br))
            {
                br.UpdateSize();
            }
        } else if (originalParent.GetComponent<InputField>() != null && transform.parent != originalParent) {
            DropInto dropHandler = originalParent.GetComponentInChildren<DropInto>();
            if (dropHandler != null)
            {
                dropHandler.RemoveBlock();
            }
        }
        droppedInto = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        foreach (var item in getCanvas().GetComponentsInChildren<Code>())
        {
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        foreach (var item in getCanvas().GetComponentsInChildren<Parameter>())
        {
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
        canvasGroup.alpha = 0.6f;
    }

    private Canvas getCanvas() {
        Canvas parentCanvas;
        Transform t = transform;
        while (true) {
            if (t.parent.TryGetComponent(out Canvas c)) {
                parentCanvas = c;
                break;
            }
            t = t.parent;
        }
        return parentCanvas;
    }

    public virtual void OnPointerUp(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
        canvasGroup.alpha = 1f;
    }
}