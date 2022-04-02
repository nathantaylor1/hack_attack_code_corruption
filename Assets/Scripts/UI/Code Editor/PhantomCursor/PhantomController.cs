using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    public static PhantomController instance;
    protected RectTransform rect;
    // Start is called before the first frame update
    public List<StartPhantomCursor> starts = new List<StartPhantomCursor>();
    public List<StopPhantomCursor> ends = new List<StopPhantomCursor>();
    RectTransform curStart;
    RectTransform curEnd;
    bool done = false;
    public bool moving = false;
    public bool close = false;
    public float dist = 10f;
    public float speed = 0.001f;
    public int scale = 10;
    void Awake()
    {
        instance = this;
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void StartMoving() {
        if (moving) {
            return;
        }
        moving = true;
        gameObject.SetActive(true);
        
        starts.Sort((x, y) => -x.order.CompareTo(y.order));
        curStart = (RectTransform)starts[starts.Count - 1].transform;
        starts.RemoveAt(starts.Count -1);
        
        ends.Sort((x, y) => -x.order.CompareTo(y.order));
        curEnd = (RectTransform)ends[ends.Count - 1].transform;
        ends[ends.Count - 1].ended.AddListener(WaitForEnd);
        StartCoroutine(Move());
    }

    public void WaitForEnd() {
        ends[ends.Count - 1].ended.RemoveListener(WaitForEnd);
        ends.RemoveAt(ends.Count - 1);
        done = true;
        moving = false;
    }

    IEnumerator Move() {
        while (!done) {
            if (!EditorController.instance.is_in_editor) {
                yield return null;
            }
            var start = curStart.position + Vector3.right * scale + Vector3.down * scale / 3f;
            var end = curEnd.position  + Vector3.right * scale + Vector3.down * scale / 3f;
            // bool close = false;
            rect.position = start;
            yield return new WaitForSecondsRealtime(.1f);
            // var end = Camera.main.ScreenToWorldPoint(curEnd.position);
            float tot = 0;
            while (!close) {
                tot += Time.unscaledDeltaTime * speed;
                rect.position = Vector2.Lerp(start, end, tot);
                if (Vector2.Distance(rect.position, end) < dist) {
                    close = true;
                }
                yield return null;
            }
            close = false;
            yield return new WaitForSecondsRealtime(.1f);
        }
        done = false;

        // Go to next action if you can
        if (starts.Count > 0) {
            StartMoving();
        } else {
            gameObject.SetActive(false);
        }
    }
}
