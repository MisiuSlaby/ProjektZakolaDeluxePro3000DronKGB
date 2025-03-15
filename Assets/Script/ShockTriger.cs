using UnityEngine;
using System.Collections;

public class SockTriger : MonoBehaviour
{
    [SerializeField] private Material shockMaterial;
    // Okre�l czas trwania efektu
    [SerializeField] private float effectDuration = 1.0f;

    // Zapami�tujemy oryginalny materia�
    private Material originalMaterial;

    void Start()
    {
        // Przyjmujemy, �e SpriteRenderer jest na tym samym obiekcie
        originalMaterial = GetComponent<SpriteRenderer>().material;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GameObject collided with " + collision.name);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Zmieniamy materia� na shockMaterial
            spriteRenderer.material = shockMaterial;
            // Uruchamiamy coroutine efektu
            StartCoroutine(SmoothWaveEffect());
        }
    }

    private IEnumerator SmoothWaveEffect()
    {
        float elapsedTime = 0f;
        // Ustawiamy pocz�tkowy stan dla efektu
        shockMaterial.SetFloat("_WaveDistanceFromCenter", 0f);

        while (elapsedTime < effectDuration)
        {
            // Obliczamy interpolowan� warto��
            float value = Mathf.Lerp(0f, 1f, elapsedTime / effectDuration);
            shockMaterial.SetFloat("_WaveDistanceFromCenter", value);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ustawiamy ostateczn� warto�� na 1f
        shockMaterial.SetFloat("_WaveDistanceFromCenter", 1f);

        // Opcjonalnie mo�esz odczeka� chwile na efekt ko�cowego stanu
        yield return new WaitForSeconds(0.5f);

        // Przywracamy oryginalny materia�
        GetComponent<SpriteRenderer>().material = originalMaterial;
    }
}
