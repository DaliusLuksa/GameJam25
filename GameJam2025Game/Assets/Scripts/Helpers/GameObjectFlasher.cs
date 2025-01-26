using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectFlasher
{
    private static readonly Dictionary<GameObject, FlashingBehaviour> FlashingObjects = new Dictionary<GameObject, FlashingBehaviour>();

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
            // Start flashing
            if (!FlashingObjects.ContainsKey(target))
            {
                FlashingBehaviour flasher = target.AddComponent<FlashingBehaviour>();
                FlashingObjects[target] = flasher;
                flasher.StartFlash(renderer, flashColor, flashDuration);
            }
        }
        else
        {
            // Stop flashing
            if (FlashingObjects.ContainsKey(target))
            {
                FlashingObjects[target].StopFlash(renderer);
                Object.Destroy(FlashingObjects[target]); // Remove the behavior
                FlashingObjects.Remove(target);
            }
        }
    }
}

public class FlashingBehaviour : MonoBehaviour
{
    private Coroutine flashCoroutine;
    private Color originalColor;

    // Start the flashing effect
    public void StartFlash(Renderer renderer, Color flashColor, float flashDuration)
    {
        // If a coroutine is already running, stop it first
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        // Store the original color and start the flashing coroutine
        originalColor = renderer.material.color;
        flashCoroutine = StartCoroutine(FlashEffect(renderer, flashColor, flashDuration));
    }

    // Stop the flashing effect
    public void StopFlash(Renderer renderer)
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        // Restore the original color
        if (renderer != null && renderer.material != null)
        {
            renderer.material.color = originalColor;
        }
    }

    // Coroutine for the flashing effect
    private IEnumerator FlashEffect(Renderer renderer, Color flashColor, float flashDuration)
    {
        while (true)
        {
            if (renderer.material != null)
            {
                // Flash color
                renderer.material.color = flashColor;
                yield return new WaitForSeconds(flashDuration / 2);

                // Revert to original color
                renderer.material.color = originalColor;
                yield return new WaitForSeconds(flashDuration / 2);
            }
        }
    }
}
