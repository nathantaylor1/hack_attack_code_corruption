using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    public bool isEndOfLevel = false;
    bool called = false;
    // Currently set to restart level for gold_spike purposes
    // Can also be set to activate on button click
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (!called && isEndOfLevel)
            {
                called = true;
                GameManager.instance.LevelCompleted();
            }
            else if (!GameManager.instance.reloading)
            {
                Debug.Log("invoked");
                GameManager.instance.GoBackToPreviousCheckpoint();
            }
        }
    }
}
