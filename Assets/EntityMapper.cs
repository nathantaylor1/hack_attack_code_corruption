using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMapper : MonoBehaviour
{
    protected static EntityMapper instance;
    [System.Serializable]
    public class Entity
    {
        public EntityType entityType;
        public Sprite entityIcon;
    }
    [SerializeField]
    protected List<Entity> entities = new List<Entity>();
    protected static Dictionary<EntityType, Sprite> entityMap = new Dictionary<EntityType, Sprite>();

    private void Awake()
    {
        foreach(Entity e in entities)
        {
            entityMap.Add(e.entityType, e.entityIcon);
        }
    }

    public static Sprite GetEntitySprite(EntityType _entityType)
    {
        if (!entityMap.ContainsKey(_entityType)) return null;
        return entityMap[_entityType];
    }
}
