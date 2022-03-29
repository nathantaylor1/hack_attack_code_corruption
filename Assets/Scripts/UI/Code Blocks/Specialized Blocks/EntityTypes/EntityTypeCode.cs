using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EntityTypeCode : Code
{
    public EntityType entityType = EntityType.Player;
    [SerializeField] 
    protected Image entityImage;
    private TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();

        if (EntityMapper.GetEntitySprite(entityType) == null)
        {
            SetText();
            entityImage.enabled = false;
        }
        else
        {
            entityImage.sprite = EntityMapper.GetEntitySprite(entityType);
        }
    }

    public override dynamic ReturnValue()
    {
        return entityType.GetLayer();
    }

    private void SetText()
    {
        text.text = LayerMask.LayerToName(entityType.GetLayer());
    }
}
