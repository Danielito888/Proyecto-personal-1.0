using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI energyText;

    void Update()
    {
        waterText.text = "Water: " + ResourceManager.Instance.water;
        foodText.text = "Food: " + ResourceManager.Instance.food;
        energyText.text = "Energy: " + ResourceManager.Instance.energy;
    }
}
