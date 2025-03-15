using UnityEngine;
using System.Collections;

public class SockTriger : MonoBehaviour
{
    [SerializeField] private Material shockMaterial;
    // Okreœl czas trwania efektu
    [SerializeField] private float effectDuration = 1.0f;

    // Zapamiêtujemy oryginalny materia³
    private Material originalMaterial;

    void Start()
    {
        // Przyjmujemy, ¿e SpriteRenderer jest na tym samym obiekcie
        originalMaterial = GetComponent<SpriteRenderer>().material;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GameObject collided with " + collision.name);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Zmieniamy materia³ na shockMaterial
            spriteRenderer.material = shockMaterial;
            // Uruchamiamy coroutine efektu
            StartCoroutine(SmoothWaveEffect());
        }
    }

    private IEnumerator SmoothWaveEffect()
    {
        float elapsedTime = 0f;
        // Ustawiamy pocz¹tkowy stan dla efektu
        shockMaterial.SetFloat("_WaveDistanceFromCenter", 0f);

        while (elapsedTime < effectDuration)
        {
            // Obliczamy interpolowan¹ wartoœæ
            float value = Mathf.Lerp(0f, 1f, elapsedTime / effectDuration);
            shockMaterial.SetFloat("_WaveDistanceFromCenter", value);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ustawiamy ostateczn¹ wartoœæ na 1f
        shockMaterial.SetFloat("_WaveDistanceFromCenter", 1f);

        // Opcjonalnie mo¿esz odczekaæ chwile na efekt koñcowego stanu
        yield return new WaitForSeconds(0.5f);

        // Przywracamy oryginalny materia³
        GetComponent<SpriteRenderer>().material = originalMaterial;
    }
}
