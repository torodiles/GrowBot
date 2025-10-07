using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public EdgeCollider2D destinationBoundary;

    private CameraBoundary cameraScript;

    void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraBoundary>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (destination == null)
            {
                Debug.LogError("Tujuan Teleportasi belum ditentukan!");
                return;
            }

            other.transform.position = destination.position;

            cameraScript.UpdateBoundary(destinationBoundary);
        }
    }
}
