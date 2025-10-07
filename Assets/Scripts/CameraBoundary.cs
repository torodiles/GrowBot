using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraBoundary : MonoBehaviour
{
    public Transform target;
    public EdgeCollider2D boundaryCollider;

    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private bool isBoundaryActive = true;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (boundaryCollider != null)
        {
            CalculateBounds();
        }
    }

    private void CalculateBounds()
    {
        Bounds colliderBounds = boundaryCollider.bounds;
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

        minBounds.x = colliderBounds.min.x + cameraHalfWidth;
        minBounds.y = colliderBounds.min.y + cameraHalfHeight;
        maxBounds.x = colliderBounds.max.x - cameraHalfWidth;
        maxBounds.y = colliderBounds.max.y - cameraHalfHeight;
    }

    public void UpdateBoundary(EdgeCollider2D newBoundary)
    {
        if (newBoundary != null)
        {
            boundaryCollider = newBoundary;
            CalculateBounds();
            isBoundaryActive = true;
        }
        else
        {
            isBoundaryActive = false;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (isBoundaryActive && boundaryCollider != null)
        {
            Vector3 targetPosition = target.position;
            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
