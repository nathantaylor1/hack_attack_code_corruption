using TMPro;
using UnityEngine;
public class EntityTypeCode : Code
{
    public EntityType entityType = EntityType.Player;
    private TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        SetText();
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
