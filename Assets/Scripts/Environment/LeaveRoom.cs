using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    public bool isEndOfLevel = false;
    // Currently set to restart level for gold_spike purposes
    // Can also be set to activate on button click
    private void OnTriggerStay2D(Collider2D other) {
        if (isEndOfLevel)
        {
            // Unity Analytics Send Level Complete
            AnalyticsCollection.LevelComplete(SceneManager.GetActiveScene().buildIndex);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
