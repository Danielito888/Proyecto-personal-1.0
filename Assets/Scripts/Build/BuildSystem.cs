using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    private BuildableModule selectedModule;
    public LayerMask placeableLayer;
    public LayerMask invalidLayers; 
    public GameObject buildHintText;
    public Camera playerCamera;
    public PlayerInventory playerInventory;

    [Header("Hologram")]
    public Material hologramMaterial;
    public Material invalidMaterial;
    public float rotationSpeed = 90f;
    private GameObject currentHologram;
    private float currentRotation = 0f;
    private bool validPlacement = false;

    public void StartPlacing(BuildableModule module)
    {
        selectedModule = module;
        if (currentHologram != null) Destroy(currentHologram);

        currentHologram = Instantiate(module.prefab);
        SetLayerRecursively(currentHologram, LayerMask.NameToLayer("Hologram"));
        SetHologramMaterial(currentHologram, hologramMaterial);
        currentRotation = 0f;

        if (buildHintText != null)
            buildHintText.SetActive(true);
    }

    public void CancelPlacement()
    {
        if (currentHologram != null) Destroy(currentHologram);
        currentHologram = null;
        selectedModule = null;

        if (buildHintText != null)
            buildHintText.SetActive(false);
    }

    void Update()
    {
        if (currentHologram == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 5f, placeableLayer))
        {
            Vector3 targetPosition = hit.point;
            currentHologram.transform.position = targetPosition;
            currentHologram.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

            validPlacement = IsPlacementValid(currentHologram);

            SetHologramMaterial(currentHologram, validPlacement ? hologramMaterial : invalidMaterial);

            if (Input.GetKeyDown(KeyCode.Mouse0) && validPlacement)
            {
                if (playerInventory.HasResources(selectedModule.cost))
                {
                    GameObject placedObject = Instantiate(selectedModule.prefab, targetPosition, Quaternion.Euler(0f, currentRotation, 0f));
                    placedObject.GetComponent<BoxCollider>().isTrigger = false;
                    SetLayerRecursively(placedObject, LayerMask.NameToLayer("PlacedModule"));
                    playerInventory.ConsumeResources(selectedModule.cost);
                    Destroy(currentHologram);
                    currentHologram = null;
                    selectedModule = null;
                    buildHintText.SetActive(false);
                }
                else
                {
                    Debug.Log("No tienes suficientes recursos.");
                }
            }

            HandleRotationInput();
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null) continue;
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    void SetHologramMaterial(GameObject obj, Material mat)
    {
        foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
        {
            r.material = mat;
        }
    }

    void HandleRotationInput()
    {
        float direction = 0f;

        if (Input.GetKey(KeyCode.R))
        {
            direction = Input.GetKey(KeyCode.LeftShift) ? -1f : 1f;
        }

        if (direction != 0f)
        {
            currentRotation += direction * rotationSpeed * Time.deltaTime;
            currentHologram.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
        }
    }

    bool IsPlacementValid(GameObject hologram)
    {
        Collider[] overlaps = Physics.OverlapBox(
            hologram.transform.position,
            hologram.GetComponent<Collider>().bounds.extents * 0.9f,
            hologram.transform.rotation,
            invalidLayers 
        );

        foreach (var col in overlaps)
        {
            if (col.gameObject == hologram) continue;

            if (((1 << col.gameObject.layer) & invalidLayers) != 0)
            {
                return false;
            }
        }

        return true;
    }


}
