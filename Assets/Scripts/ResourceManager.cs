using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int water = 10;
    public int food = 5;
    public int energy = 3;

    public static ResourceManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}

