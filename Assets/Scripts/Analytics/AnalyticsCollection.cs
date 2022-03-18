using UnityEngine;
using UnityEngine.Analytics;

// https://docs.unity3d.com/ScriptReference/Analytics.Analytics-enabled.html
public class AnalyticsCollection : MonoBehaviour {
    
    // If you want to disable Analytics completely during runtime
    public static void DisableAnalyticsCompletely()
    {
        Analytics.enabled = false;
        Analytics.deviceStatsEnabled = false;
        PerformanceReporting.enabled = false;
    }
}