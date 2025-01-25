using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResolutionAdaptiveCamera : MonoBehaviour
{
    private Camera mainCamera;
    private float initialAspect;
    private float initialOrthographicSize;

    void Start()
    {
        // Get the main camera
        mainCamera = GetComponent<Camera>();

        // Store the initial aspect ratio and orthographic size
        initialAspect = (float)Screen.width / Screen.height;
        initialOrthographicSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        // Calculate the current aspect ratio
        float currentAspect = (float)Screen.width / Screen.height;

        if (mainCamera.orthographic)
        {
            // Adjust orthographic size to maintain the same vertical size
            mainCamera.orthographicSize = initialOrthographicSize * (initialAspect / currentAspect);
        }
        else
        {
            // Adjust field of view for perspective cameras
            float initialFOV = Mathf.Atan(Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad / 2) * initialAspect);
            mainCamera.fieldOfView = Mathf.Rad2Deg * 2 * Mathf.Atan(Mathf.Tan(initialFOV) / currentAspect);
        }
    }
}
