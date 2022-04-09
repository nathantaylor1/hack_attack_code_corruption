using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    private void Awake()
    {
        HasHealth health = GetComponentInParent<HasHealth>();
        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
            health.OnRevive.AddListener(OnRevive);
        }
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnRevive()
    {
        gameObject.SetActive(true);
    }
}
