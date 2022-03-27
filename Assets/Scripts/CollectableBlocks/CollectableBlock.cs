using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlock : MonoBehaviour
{
    public GameObject block;
    private bool alreadyPickedUp = false;
    public float quick = 1.6f;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            AddToInventory();
        } else if (alreadyPickedUp && other.gameObject.layer == LayerMask.NameToLayer("Collectables")) {
            // if collides with remove block
            Destroy(other.gameObject);
        }
    }

    public virtual void AddToInventory()
    {
        if (alreadyPickedUp) return;
        alreadyPickedUp = true;
        InventoryManager.instance.AddBlock(gameObject.transform.GetChild(0).GetComponent<Code>());
        StartCoroutine(moveToward());
    }

    private IEnumerator moveToward() {
        var close = false;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f + Vector3.right * 1);
        while (!close) {
            var temp = Camera.main.ScreenToWorldPoint(screenPos);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, Time.fixedDeltaTime);
            if (Vector3.Distance(temp, transform.position) < 1f) {
                close = true;
            }
            yield return null;
        }
        close = false;
        while (!close) {
            var temp = Camera.main.ScreenToWorldPoint(block.transform.position);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, Time.fixedDeltaTime * quick);
            if (Vector3.Distance(temp, transform.position) < 1f) {
                close = true;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
