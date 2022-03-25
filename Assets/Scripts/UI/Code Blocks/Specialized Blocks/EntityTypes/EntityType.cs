using UnityEngine;

public enum EntityType
{
    Player,
    Enemy,
    MouseCursor,
    Collectables,
    Movables
}

public static class EntityTypeClass
{
    public static int GetLayer(this EntityType type)
    {
        switch (type)
        {
            case EntityType.Player:
                return LayerMask.NameToLayer("Player");
            
            case EntityType.Enemy:
                return LayerMask.NameToLayer("Enemies");
            
            case EntityType.MouseCursor:
                return LayerMask.NameToLayer("Cursor");
            
            case EntityType.Collectables:
                return LayerMask.NameToLayer("Collectables");
            
            case EntityType.Movables:
                return LayerMask.NameToLayer("Movables");
            
            default:
                Debug.LogError("EntityType.GetLayer called with invalid EntityType.");
                return -1;
        }
    }
}