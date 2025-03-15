using UnityEngine;
using Cinemachine;

public class DynamicFOV : MonoBehaviour
{
    [Header("Ustawienia FOV")]
    [Tooltip("Referencja do obiektu Cinemachine Virtual Camera")]
    public CinemachineVirtualCamera virtualCamera;

    [Tooltip("Domyœlne pole widzenia (FOV) kamery")]
    public float defaultFOV = 60f;

    [Tooltip("FOV kamery, gdy spacja jest wciœniêta")]
    public float zoomedInFOV = 30f;

    [Tooltip("Prêdkoœæ zmiany FOV")]
    public float fovChangeSpeed = 2f;

    // Wartoœæ docelowa dla FOV
    private float targetFOV;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Brak referencji do Cinemachine Virtual Camera!");
            return;
        }

        // Ustawienie domyœlnego FOV
        virtualCamera.m_Lens.FieldOfView = defaultFOV;
        targetFOV = defaultFOV;

        // Upewnij siê, ¿e kamera jest ustawiona na tryb perspektywiczny
        if (virtualCamera.m_Lens.Orthographic)
        {
            Debug.LogWarning("Kamera jest ustawiona na ortograficzn¹. Funkcja FOV dzia³a tylko w trybie perspektywicznym.");
        }
    }

    void Update()
    {
        // SprawdŸ, czy spacja jest wciœniêta
        if (Input.GetKey(KeyCode.Space))
        {
            // Ustaw docelowe FOV na wartoœæ przy zoomie
            targetFOV = zoomedInFOV;
        }
        else
        {
            // Jeœli spacja nie jest wciœniêta, wróæ do domyœlnego FOV
            targetFOV = defaultFOV;
        }

        // P³ynnie interpoluj aktualne FOV do targetFOV
        float currentFOV = virtualCamera.m_Lens.FieldOfView;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * fovChangeSpeed);
    }
}
