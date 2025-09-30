using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleCost
{
    public string resourceName;
    public int amount;
}

[System.Serializable]
public class BuildableModule
{
    public GameObject prefab;
    public List<ModuleCost> cost;
    public string moduleName;
}
