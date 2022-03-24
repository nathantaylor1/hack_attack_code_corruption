using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
// https://docs.unity3d.com/ScriptReference/Analytics.Analytics-enabled.html

public class AnalyticsCollection : MonoBehaviour {

    public static void LevelComplete(int levelNum)
    {
        AnalyticsResult res = Analytics.CustomEvent("LevelComplete", 
            new Dictionary<string, object>()
            {
                {"Level", levelNum}
            });
        Debug.Log("analyticsResult: " + res);
    }

    private static float timeOpened;
    public static void OpenedEditor()
    {
        timeOpened = Time.realtimeSinceStartup;
    }

    public static void ClosedEditor()
    {
        float timeInEditor = Time.realtimeSinceStartup - timeOpened;
        AnalyticsResult res = Analytics.CustomEvent("EditorTime", 
            new Dictionary<string, object>()
            {
                {"Time In Editor", timeInEditor}
            });
        Debug.Log("analyticsResult: " + res);
    }
    
    // If you want to disable Analytics completely during runtime
    public static void DisableAnalyticsCompletely()
    {
        Analytics.enabled = false;
        Analytics.deviceStatsEnabled = false;
        PerformanceReporting.enabled = false;
    }
}