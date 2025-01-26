using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectFlasher
{
    private static readonly Dictionary<GameObject, Coroutine> FlashingObjects = new Dictionary<GameObject, Coroutine>();

    // Static method to start or stop flashing
    public static void SetGameObjectFlashing(GameObject target, bool isFlashing, Color flashColor, float flashDuration = 0.5f)
    {
        if (target == null)
        {
            Debug.LogWarning("Target GameObject is null.");
            return;
        }

        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogWarning($"The GameObject '{target.name}' does not have a Renderer component.");
            return;
        }

        if (isFlashing)
        {
            // Start flashing if not already flashing
            if (!FlashingObjects.ContainsKey(target))
            {
                Coroutine flashCoroutine = target.AddComponent<FlashingBehaviour>().StartFlash(renderer, flashColor, flashDuration);
                FlashingObjects[target] = flashCoroutine;
            }
        }
        else
        {
            // Stop flashing if it's currently flashing
            if (FlashingObjects.ContainsKey(target))
            {
                target.GetComponent<FlashingBehaviour>().StopFlash(renderer);
                Object.Destroy(target.GetComponent<FlashingBehaviour>()); // Cleanup temporary behavior
                FlashingObjects.Remove(target);
            }
        }
    }
}

// Helper MonoBehaviour for the flashing logic
public class FlashingBehaviour : MonoBehaviour
{
    private Coroutine flashCoroutine;

    // Start the flash coroutine
    public Coroutine StartFlash(Renderer renderer, Color flashColor, float flashDuration)
    {
        flashCoroutine = StartCoroutine(FlashEffect(renderer, flashColor, flashDuration));
        return flashCoroutine;
    }

    // Stop the flash coroutine
    public void StopFlash(Renderer renderer)
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;

            // Restore the original color
            if (renderer != null)
            {
                Material material = renderer.material;
                material.color = material.color; // Restore cached color
                //material.color = Color.white; // fix because randomly objects stay red or green
            }
        }
    }

    // Coroutine to handle the flashing effect
    private IEnumerator FlashEffect(Renderer renderer, Color flashColor, float flashDuration)
    {
        Material material = renderer.material;
        Color originalColor = material.color;

        while (true)
        {
            // Change to flash color
            material.color = flashColor;
            yield return new WaitForSeconds(flashDuration / 2);

            // Revert to original color
            material.color = originalColor;
            yield return new WaitForSeconds(flashDuration / 2);
        }
    }
}
