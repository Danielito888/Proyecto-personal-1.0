using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text resourceText;

    void Update()
    {
        string display = "";
        foreach (var res in InventorySystem.Instance.resources)
        {
            display += $"{res.name}: {res.amount}\n";
        }
        resourceText.text = display;
    }
}
