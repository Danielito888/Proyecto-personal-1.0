using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [System.Serializable]
    public class Category
    {
        public string name;
        public BuildableModule[] modules; // CAMBIO: Antes GameObject[], ahora BuildableModule[]
    }

    public Category[] categoriesArray;
    private Dictionary<string, BuildableModule[]> categoriesDict;

    public GameObject buttonPrefab;
    public Transform moduleGrid;
    public GameObject menuUI;
    public BuildSystem buildSystem;

    void Start()
    {
        // Convertir array en diccionario para acceso por nombre
        categoriesDict = new Dictionary<string, BuildableModule[]>();
        foreach (Category cat in categoriesArray)
        {
            categoriesDict[cat.name] = cat.modules;
        }

        // Mostrar la primera categoría automáticamente
        if (categoriesArray.Length > 0)
        {
            ShowCategory(categoriesArray[0].name);
        }
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenMenu()
    {
        menuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (categoriesArray.Length > 0)
        {
            ShowCategory(categoriesArray[0].name);
        }

        buildSystem.CancelPlacement();
    }

    public void ShowCategory(string categoryName)
    {
        if (!categoriesDict.ContainsKey(categoryName)) return;

        // Limpiar botones anteriores
        foreach (Transform child in moduleGrid)
        {
            Destroy(child.gameObject);
        }

        // Crear botones nuevos
        foreach (BuildableModule module in categoriesDict[categoryName])
        {
            GameObject btn = Instantiate(buttonPrefab, moduleGrid);
            btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = module.moduleName;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                buildSystem.StartPlacing(module);
                CloseMenu();
            });
        }
    }

    public void OnCategoryButtonClick(string categoryName)
    {
        ShowCategory(categoryName);
    }
}
