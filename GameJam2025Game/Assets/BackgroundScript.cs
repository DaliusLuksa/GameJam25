using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 50f; // Speed at which the background moves
    public float resetPositionY = -1080f; // The Y position at which the background resets (adjust as needed)
    public float startPositionY = 1080f; // The Y position to move the background back to

    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform component attached to this UI element
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("BackgroundScroller script must be attached to a GameObject with a RectTransform!");
        }
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // Move the background upwards
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.y += speed * Time.deltaTime;

            // Reset position if it goes beyond the reset point
            if (newPosition.y > startPositionY)
            {
                newPosition.y = resetPositionY;
            }

            // Apply the new position
            rectTransform.localPosition = newPosition;
        }
    }
}