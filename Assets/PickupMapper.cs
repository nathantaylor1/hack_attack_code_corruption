using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMapper : MonoBehaviour
{
    protected static PickupMapper instance;
    [System.Serializable]
    public class Pickup
    {
        public string returnType;
        public Sprite sprite;
    }
    [SerializeField]
    protected List<Pickup> pickups = new List<Pickup>();
    protected static Dictionary<string, Sprite> pickupMap = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            foreach (Pickup p in pickups)
            {
                if (!pickupMap.ContainsKey(p.returnType))
                {
                    pickupMap.Add(p.returnType, p.sprite);
                }
            }
        }
        instance = this;
    }

    public static Sprite GetPickupSprite(string returnType)
    {
        if (!pickupMap.ContainsKey(returnType)) return null;
        return pickupMap[returnType];
    }
}
