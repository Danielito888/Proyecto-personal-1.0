using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [System.Serializable]
    public class Resource
    {
        public string name;
        public int amount;
    }

    public List<Resource> resources = new List<Resource>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool HasResource(string resourceName, int amount)
    {
        Resource res = resources.Find(r => r.name == resourceName);
        return res != null && res.amount >= amount;
    }

    public void AddResource(string resourceName, int amount)
    {
        Resource res = resources.Find(r => r.name == resourceName);
        if (res != null)
            res.amount += amount;
        else
            resources.Add(new Resource { name = resourceName, amount = amount });
    }

    public bool ConsumeResource(string resourceName, int amount)
    {
        if (HasResource(resourceName, amount))
        {
            Resource res = resources.Find(r => r.name == resourceName);
            res.amount -= amount;
            return true;
        }
        return false;
    }

    public int GetAmount(string resourceName)
    {
        Resource res = resources.Find(r => r.name == resourceName);
        return res != null ? res.amount : 0;
    }
}
