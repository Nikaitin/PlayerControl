using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [Header("Settings")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private SpriteRenderer spriteRen;
    private Color originalColor;
    private Coroutine _flashRoutine;

    void Awake()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        originalColor = spriteRen.color;
    }

    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }
        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRen.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRen.color = originalColor;

        _flashRoutine = null;
    }
}
