using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterSeconds : MonoBehaviour
{
    public float seconds;
    
    private void Start() {
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
