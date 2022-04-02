using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlock : MonoBehaviour
{
    public GameObject block;
    public bool alreadyPickedUp = false;
    public float slow = 1.6f;
    public float quick = 2f;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            AddToInventory();
        }
    }

    public virtual void AddToInventory()
    {
        if (alreadyPickedUp) return;
        alreadyPickedUp = true;
        var p = GetComponent<printID>();
        p.Add();
        p.rewind.AddListener(Rewind);
        //Transform codeBlock = Instantiate(gameObject.transform.GetChild(0).GetChild(0));
        GameObject codeBlock = Instantiate(block);
        InventoryManager.instance.AddBlock(codeBlock.GetComponent<Code>());
        codeBlock.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(moveToward());

        if (pickupSound != null && AudioManager.instance != null && Camera.main != null)
            AudioManager.instance.PlaySound(pickupSound, Camera.main.transform.position);
    }

    void Rewind() {
        alreadyPickedUp = false;
    }

    private IEnumerator moveToward() {
        var close = false;
        var screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f + Vector3.right * 1);
        float lastTime = Time.time;
        /*while (!close) {
            float deltaTime = Time.time - lastTime;
            var temp = Camera.main.ScreenToWorldPoint(screenPos);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, deltaTime * slow);
            if (Vector3.Distance(temp, transform.position) < 1.5f) {
                close = true;
            }
            lastTime = Time.time;
            yield return null;
        }*/
        close = false;
        while (!close)
        {
            float deltaTime = Time.time - lastTime;
            var temp = Camera.main.ScreenToWorldPoint(InventoryImage.instance.transform.position);
            temp.z = 0;
            transform.position = Vector3.Lerp(transform.position, temp, deltaTime * quick);
            if (Vector3.Distance(temp, transform.position) < 1.7f) {
                close = true;
            }
            lastTime = Time.time;
            yield return null;
        }
        // Pickup blocks must be nested exactly two levels
        /*Transform codeBlock = gameObject.transform.GetChild(0).GetChild(0);
        InventoryManager.instance.AddBlock(codeBlock.GetComponent<Code>());
        codeBlock.localScale = new Vector3(1, 1, 1);*/
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
