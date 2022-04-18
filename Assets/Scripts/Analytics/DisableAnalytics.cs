using UnityEngine;
using UnityEngine.Analytics;

public class DisableAnalytics : MonoBehaviour
{
    void Start()
    {
        Analytics.enabled = false;
        Analytics.deviceStatsEnabled = false;
        PerformanceReporting.enabled = false;
    }
}
