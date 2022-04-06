using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ProjectileTypes : Code
{
    public GameObject prefab;
    [SerializeField] 
    protected Image entityImage;
    protected override void Awake() {
        base.Awake();
        entityImage.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }
    public override dynamic ReturnValue()
    {
        return prefab;
    }
}