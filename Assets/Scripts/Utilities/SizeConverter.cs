using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeConverter : MonoBehaviour
{
    public static SizeConverter instance;
    protected static Canvas canvas;
    protected static Image image;
    protected static RectTransform rect;

    protected static Vector2 pixelsToCanvasCoefficients;

    protected void Awake()
    {
        instance = this;

        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        image.type = Image.Type.Simple;
        image.SetNativeSize();

        pixelsToCanvasCoefficients = rect.rect.size / image.sprite.rect.size / canvas.scaleFactor;
    }

    public static Vector2 PixelsToCanvasSpace(Vector2 pixels)
    {
        return pixels * pixelsToCanvasCoefficients;
    }
}
