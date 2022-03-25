using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EntityTypeCode : Code
{
        [SerializeField]
    protected int entityLayer;

    protected TMP_Text text;

    void Start()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        text.text = LayerMask.LayerToName(entityLayer);
    }

    public override dynamic ReturnValue()
    {
        return entityLayer;
    }
}
