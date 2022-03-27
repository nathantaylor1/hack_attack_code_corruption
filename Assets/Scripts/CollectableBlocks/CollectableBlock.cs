using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlock : MonoBehaviour
{
    public GameObject block;
    private bool alreadyPickedUp = false;
    public float quick = 1.6f;
    public AudioClip pickupSound;

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
        StartCoroutine(moveToward());

        if (pickupSound != null && AudioManager.instance != null && Camera.main != null)
            AudioManager.instance.PlaySound(pickupSound, Camera.main.transform.position);
    }

    private IEnumerator moveToward() {
        var close = false;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f + Vector3.right * 1);
        while (!close) {
            var temp = Camera.main.ScreenToWorldPoint(screenPos);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, Time.fixedDeltaTime * 1.2f);
            if (Vector3.Distance(temp, transform.position) < 1.5f) {
                close = true;
            }
            yield return null;
        }
        close = false;
        while (!close) {
            var temp = Camera.main.ScreenToWorldPoint(block.transform.position);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, Time.fixedDeltaTime * quick);
            if (Vector3.Distance(temp, transform.position) < 1.7f) {
                close = true;
            }
            yield return null;
        }
        // Pickup blocks must be nested exactly two levels
        Transform codeBlock = gameObject.transform.GetChild(0).GetChild(0);
        InventoryManager.instance.AddBlock(codeBlock.GetComponent<Code>());
        codeBlock.localScale = new Vector3(1, 1, 1);
        Destroy(gameObject);
    }
}
