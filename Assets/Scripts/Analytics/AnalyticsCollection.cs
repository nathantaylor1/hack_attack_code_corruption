using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
// https://docs.unity3d.com/ScriptReference/Analytics.Analytics-enabled.html

public class AnalyticsCollection : MonoBehaviour
{
    public GameObject window;
    public GameObject tab;
    
    public static void LevelComplete(int levelNum)
    {
        if (Application.isEditor) return;
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
        if (Application.isEditor) return;
        timeOpened = Time.realtimeSinceStartup;
    }

    public static void ClosedEditor()
    {
        if (Application.isEditor) return;
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
        AnalyticsStorage.enabled = false;
        Analytics.deviceStatsEnabled = false;
        PerformanceReporting.enabled = false;
        AnalyticsStorage.hasAnswered = true;
    }

    public static void ReEnableAnalytics()
    {
        Analytics.enabled = true;
        AnalyticsStorage.enabled = true;
        Analytics.deviceStatsEnabled = true;
        PerformanceReporting.enabled = true;
        AnalyticsStorage.hasAnswered = true;
    }

    private void Start()
    {
        if (!AnalyticsStorage.hasAnswered)
        {
            window.SetActive(true);
            tab.SetActive(true);
        }
    }
}