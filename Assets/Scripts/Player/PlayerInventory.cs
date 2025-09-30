using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string, int> resources = new Dictionary<string, int>();

    void Start()
    {
        resources["Metal"] = 20;
        resources["Plastic"] = 10;
    }

    public bool HasResources(List<ModuleCost> costs)
    {
        foreach (var cost in costs)
        {
            if (!resources.ContainsKey(cost.resourceName) || resources[cost.resourceName] < cost.amount)
                return false;
        }
        return true;
    }

    public void ConsumeResources(List<ModuleCost> costs)
    {
        foreach (var cost in costs)
        {
            if (resources.ContainsKey(cost.resourceName))
                resources[cost.resourceName] -= cost.amount;
        }
    }
}
