using UnityEngine;

public class BuildInteractable : MonoBehaviour
{
    public GameObject roomPrefab;
    public Transform buildPoint;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(roomPrefab, buildPoint.position, buildPoint.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
