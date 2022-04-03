using System;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public bool instantKill = false;
    public float damageIntervalTime = 0.5f;
    public float damageAmount = 1.0f;
    public float knockUpForce = 5.0f;
    private bool _canDamage = true;
    private void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        if (!_canDamage || !col.gameObject.CompareTag("Player")) return;
        if (instantKill)
        {
            Debug.Log("Spikes instantly killing " + col.gameObject.name);
            col.gameObject.GetComponent<HasHealth>().Damage(1000f);
            return;
        }
        _canDamage = false;
        Debug.Log("Damaging " + col.gameObject.name);
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockUpForce, ForceMode2D.Impulse);
        col.gameObject.GetComponent<HasHealth>().Damage(damageAmount);
        StartCoroutine(CO_DamageInterval());
    }

    private IEnumerator CO_DamageInterval()
    {
        Debug.Log("CO_DamageInterval");
        yield return new WaitForSeconds(damageIntervalTime);
        _canDamage = true;
        yield return null;
    }
}
