using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhantomController : MonoBehaviour
{
    public static PhantomController instance;
    protected RectTransform rect;
    // Start is called before the first frame update
    static public List<StartPhantomCursor> starts = new List<StartPhantomCursor>();
    static public List<StopPhantomCursor> ends = new List<StopPhantomCursor>();

    static public HashSet<int> startsSeen = new HashSet<int>();
    static public HashSet<int> startsSeenAtCheckpoint = new HashSet<int>();
    static public HashSet<int> endsSeen = new HashSet<int>();
    static public HashSet<int> endsSeenAtCheckpoint = new HashSet<int>();
    static int count = 0;
    static int countAtCheckpoint = 0;

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
        GetComponent<Image>().enabled = false;
        moving = false;
        CheckpointManager.PlayerKilled.AddListener(Rewind);
        CheckpointManager.CheckpointUpdated.AddListener(UpdateInfo);
    }


    private void OnDestroy() {
        CheckpointManager.PlayerKilled.RemoveListener(Rewind);
        CheckpointManager.CheckpointUpdated.RemoveListener(UpdateInfo);
    }
    public void UpdateInfo() {
        if (instance != this) {
            CheckpointManager.CheckpointUpdated.RemoveListener(UpdateInfo);
        }
        Debug.Log("update");
        startsSeenAtCheckpoint = new HashSet<int>(startsSeen);
        endsSeenAtCheckpoint = new HashSet<int>(endsSeen);
        starts = new List<StartPhantomCursor>();
        ends = new List<StopPhantomCursor>();
        countAtCheckpoint = count;
    }

    public void Rewind() {
        if (instance != this) {
            CheckpointManager.PlayerKilled.RemoveListener(Rewind);
        }
        count = countAtCheckpoint;
        startsSeen = new HashSet<int>(startsSeenAtCheckpoint);
        endsSeen = new HashSet<int>(endsSeenAtCheckpoint);
        starts = new List<StartPhantomCursor>();
        ends = new List<StopPhantomCursor>();
    }

    public static void AddStart(StartPhantomCursor sp) {
        if (!startsSeen.Contains(sp.order)) {
            Debug.Log("Adding start" + sp.order);
            starts.Add(sp);
            instance.StartMoving();
        }
    }

    public static void AddEnd(StopPhantomCursor sp) {
        if (!endsSeen.Contains(sp.order)) {
            Debug.Log("Adding end" + sp.order);
            ends.Add(sp);
            instance.StartMoving();
        }
    }

    public void StartMoving() {
        if (moving || instance != this || ends.Count == 0 || starts.Count == 0) {
            return;
        }
        starts.Sort((x, y) => -x.order.CompareTo(y.order));
        ends.Sort((x, y) => -x.order.CompareTo(y.order));
        if (starts[starts.Count - 1].order != count || ends[ends.Count - 1].order != count) {
            return;
        }

        moving = true;
        GetComponent<Image>().enabled = true;
        
        curStart = (RectTransform)starts[starts.Count - 1].transform;
        Debug.Log(starts[starts.Count - 1].order);

        
        Debug.Log(ends[ends.Count - 1].order);
        curEnd = (RectTransform)ends[ends.Count - 1].transform;
        ends[ends.Count - 1].ended.AddListener(WaitForEnd);
        StartCoroutine(Move());
    }

    public void WaitForEnd() {
        Debug.Log("end");
        count += 1;
        ends.Sort((x, y) => -x.order.CompareTo(y.order));
        starts.Sort((x, y) => -x.order.CompareTo(y.order));
        endsSeen.Add(ends[ends.Count - 1].order);
        startsSeen.Add(starts[starts.Count - 1].order);
        ends[ends.Count - 1].ended.RemoveListener(WaitForEnd);
        ends.RemoveAt(ends.Count - 1);
        starts.RemoveAt(starts.Count -1);
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
            GetComponent<Image>().enabled = false;
        }
    }
}
