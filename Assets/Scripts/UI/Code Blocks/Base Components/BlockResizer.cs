using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockResizer : MonoBehaviour
{
    protected static int cv = 0;

    public void UpdateSize()
    {
        StartCoroutine(WaitAndRebuild(transform));
    }

    private void Awake() {
        UpdateSize();
    }

    private IEnumerator WaitAndRebuild(Transform _transform)
    {
        while (cv > 20)
        {
            yield return null;
        }
        cv++;
        Debug.Log("Rebuilding " + gameObject.name);
        LayoutRebuilder.MarkLayoutForRebuild(_transform as RectTransform);
        yield return new WaitForEndOfFrame();
        cv--;
        BlockResizer br = _transform.parent.GetComponentInParent<BlockResizer>();
        if (br != null)
        {
            br.UpdateSize();
        }
    }

    /*protected Canvas canvas;

    [SerializeField]
    [Tooltip("If this block can have multiple inner blocks, the nonBlockStackingAxis is " +
        "the direction in which the blocks stack.")]
    protected RectTransform.Axis blockStackingAxis;
    protected RectTransform.Axis nonBlockStackingAxis;
    protected Vector2 criticalDim;
    protected Vector2 nonCriticalDim;

    protected Sprite startImage;
    [SerializeField]
    [Tooltip("The widths of the block image's left and right borders, respectively.")]
    protected Vector2 horizontalBorders;
    [SerializeField]
    [Tooltip("The widths of the block image's top and bottom borders, respectively.")]
    protected Vector2 verticalBorders;
    // The combined width of both horizontal and vertical borders, respectively
    protected Vector2 borderSize;
    // The size of the block without any inner blocks added
    protected Vector2 defaultSize;

    protected SortedSet<float> criticalDims = new SortedSet<float>();
    protected RectTransform rect;

    protected virtual void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        startImage = GetComponent<Image>().sprite;
        rect = GetComponent<RectTransform>();
        // ONLY WORKS IF BLOCK IS ORIGINAL SPRITE SIZE
        // Left, right
        //horizontalBorders = (new Vector2(startImage.border.x, startImage.border.z) - Vector2.one) / startImage.rect.size * rect.rect.size / canvas.scaleFactor;
        horizontalBorders = SizeConverter.PixelsToCanvasSpace(new Vector2(startImage.border.x, startImage.border.z));// - Vector2.one);
        // Top, bottom
        //verticalBorders = (new Vector2(startImage.border.w, startImage.border.y) - Vector2.one) / startImage.rect.size * rect.rect.size / canvas.scaleFactor;
        verticalBorders = SizeConverter.PixelsToCanvasSpace(new Vector2(startImage.border.w, startImage.border.y));// - Vector2.one);

        defaultSize = rect.rect.size;
        switch (blockStackingAxis)
        {
            case RectTransform.Axis.Horizontal:
                nonBlockStackingAxis = RectTransform.Axis.Vertical;
                criticalDim = Vector2.up;
                break;
            case RectTransform.Axis.Vertical:
                nonBlockStackingAxis = RectTransform.Axis.Horizontal;
                criticalDim = Vector2.right;
                break;
        }
        nonCriticalDim = Vector2.one - criticalDim;
        borderSize = new Vector2(horizontalBorders.x + horizontalBorders.y,
            verticalBorders.x + verticalBorders.y);
    }

    public virtual Vector4 GetBorderWidths()
    {
        return new Vector4(horizontalBorders.x, horizontalBorders.y,
            verticalBorders.x, verticalBorders.y);
    }

    protected virtual void ChangeInnerBlock(Vector2 originalSize, Vector2 newSize)
    {
        Vector2 thisOriginalSize = rect.rect.size;
        criticalDims.Remove(Vector2.Dot(originalSize, criticalDim));
        criticalDims.Add(Vector2.Dot(newSize, criticalDim));
        rect.SetSizeWithCurrentAnchors(nonBlockStackingAxis, criticalDims.Max + Vector2.Dot(borderSize, criticalDim));
        rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(rect.rect.size - originalSize + newSize, nonCriticalDim));
        //rect.sizeDelta = criticalDims.Max * criticalDim + borderSize * criticalDim +
        //    (rect.rect.size - originalSize + newSize) * nonCriticalDim;

        if (transform.parent != null &&
            transform.parent.TryGetComponent<BlockResizer>(out BlockResizer parentBlockResizer))
        {
            parentBlockResizer.ChangeInnerBlock(thisOriginalSize, rect.rect.size);
        }
    }

    public virtual void AddInnerBlock(Vector2 blockSize)
    {
        Vector2 originalSize = rect.rect.size;
        Vector2 newSize = Vector2.zero;
        criticalDims.Add(Vector2.Dot(blockSize, criticalDim));
        newSize += criticalDims.Max * criticalDim + borderSize * criticalDim;
        //rect.SetSizeWithCurrentAnchors(nonBlockStackingAxis, criticalDims.Max + Vector2.Dot(borderSize, criticalDim));
        if (criticalDims.Count == 1)
            //rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(borderSize + blockSize, nonCriticalDim));
            newSize += (borderSize + blockSize) * nonCriticalDim;
        else
            //rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(rect.rect.size + blockSize, nonCriticalDim));
            newSize += (rect.rect.size + blockSize) * nonCriticalDim;

        if (transform.parent != null &&
            transform.parent.TryGetComponent<BlockResizer>(out BlockResizer parentBlockResizer))
        {
            //Debug.Log(transform.parent.name);
            //parentBlockResizer.ChangeInnerBlock(originalSize, rect.rect.size);
            //parentBlockResizer.ChangeInnerBlock(originalSize, newSize);
        }

        rect.SetSizeWithCurrentAnchors(nonBlockStackingAxis, Vector2.Dot(newSize, criticalDim));
        rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(newSize, nonCriticalDim));
    }

    public virtual void RemoveInnerBlock(Vector2 blockSize)
    {
        Vector2 originalSize = rect.rect.size;
        criticalDims.Remove(Vector2.Dot(blockSize, criticalDim));
        if (criticalDims.Count == 0)
        {
            rect.SetSizeWithCurrentAnchors(nonBlockStackingAxis, Vector2.Dot(defaultSize, criticalDim));
            rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(defaultSize, nonCriticalDim));
            //rect.sizeDelta = defaultSize;
            //Debug.Log("criticalDims is empty");
        }
        else
        {
            rect.SetSizeWithCurrentAnchors(nonBlockStackingAxis, criticalDims.Max + Vector2.Dot(borderSize, criticalDim));
            rect.SetSizeWithCurrentAnchors(blockStackingAxis, Vector2.Dot(rect.rect.size - blockSize, nonCriticalDim));
        }

        if (transform.parent != null &&
            transform.parent.TryGetComponent<BlockResizer>(out BlockResizer parentBlockResizer))
        {
            //parentBlockResizer.ChangeInnerBlock(originalSize, rect.rect.size);
        }
    }*/
}
