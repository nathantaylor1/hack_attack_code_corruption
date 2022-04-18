using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryImage : MonoBehaviour
{
    public static InventoryImage instance;
    public GameObject glowLayer = null;
    protected CanvasGroup glowGroup = null;
    protected Coroutine glowCoroutine = null;

    private void Awake()
    {
        instance = this;
        EventManager.OnToggleEditor.AddListener(TurnGlowOff);
    }

    protected virtual void TurnGlowOff(bool _)
    {
        ToggleGlow(false);
    }

    public virtual void ToggleGlow(bool isGlowing)
    {
        if (isGlowing)
        {
            if (glowGroup == null)
            {
                GameObject newGlowLayer = Instantiate(glowLayer, transform);
                glowGroup = newGlowLayer.GetComponent<CanvasGroup>();
                glowCoroutine = StartCoroutine(GlowCoroutine());
            }
        }
        else
        {
            if (glowCoroutine != null)
            {
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
            glowGroup.alpha = Mathf.Sin(Time.unscaledTime * Mathf.PI) * 0.2f + 0.3f;
            yield return null;
        }
    }
}
