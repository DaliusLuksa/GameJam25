using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float intensity = 0.4f;
    private Vignette vignette;
    private Coroutine flashCoroutine;

    private void Start()
    {
        // Get the Vignette effect from the PostProcessVolume
        if (postProcessVolume.profile.TryGetSettings(out Vignette vignette))
        {
            this.vignette = vignette;
        }
        else
        {
            Debug.LogError("Vignette effect not found in the PostProcessVolume.");
        }
    }

    public void FlashVignette()
    {
        if (flashCoroutine != null)
        {
            return;
        }
        flashCoroutine = StartCoroutine(FlashVignetteCoroutine(duration, intensity));
    }

    private IEnumerator FlashVignetteCoroutine(float duration, float intensity)
    {
        float halfDuration = duration / 2f;
        float elapsedTime = 0f;

        // Fade in
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            vignette.intensity.value = Mathf.Lerp(0f, intensity, t);
            yield return null;
        }

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDuration;
            vignette.intensity.value = Mathf.Lerp(intensity, 0f, t);
            yield return null;
        }

        vignette.intensity.value = 0f;
        flashCoroutine = null;
    }
}