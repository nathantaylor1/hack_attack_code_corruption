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
    public float count = 1.2f;
    public bool moving = false;
    public bool close = false;
    public float dist = 10f;
    public float speed = 0.001f;
    public int scale = 10;
    public bool clicked = false;
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
        Debug.Log(starts[0].order);
        curStart = (RectTransform)starts[starts.Count - 1].transform;
        starts.RemoveAt(starts.Count -1);
        
        ends.Sort((x, y) => -x.order.CompareTo(y.order));
        curEnd = (RectTransform)ends[ends.Count - 1].transform;
        Debug.Log(ends[0].order);
        ends.RemoveAt(ends.Count - 1);
        
        StartCoroutine(Move());
    }

    public IEnumerator Finish() {
        yield return new WaitForSecondsRealtime(count);
        done = true;
        moving = false;
    }

    void Update()
    {
        if (EditorController.instance.is_in_editor && moving) {
            if ( !clicked && Input.GetMouseButton(0) ) {
                clicked = true;
                StartCoroutine(Finish());
            }
        }
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
        clicked = false;
        if (starts.Count > 0) {
            count = 3;
            StartMoving();
        } else {
            gameObject.SetActive(false);
        }
    }
}
