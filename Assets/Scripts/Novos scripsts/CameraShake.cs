using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private Vector3 initialLocalPos;
    private Coroutine shakeRoutine;
    private void Awake()
    {
       if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        
            Instance = this;
            initialLocalPos = transform.localPosition;
    }
    public  void Shake(float intensity, float duration)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            transform.localPosition = initialLocalPos;
        }
        shakeRoutine = StartCoroutine(ShakeCoroutine(intensity, duration));
    }
    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)

        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            float currentIntensity = Mathf.Lerp(intensity, 0f, progress);
           Vector2 offset2D = Random.insideUnitCircle * currentIntensity;
            transform.localPosition = initialLocalPos + new Vector3(offset2D.x, offset2D.y, 0f);
            
            yield return null;
        }
        transform.localPosition = initialLocalPos;
        shakeRoutine = null;
    }
}
