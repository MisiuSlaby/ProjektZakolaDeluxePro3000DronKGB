using UnityEngine;
using Cinemachine;

public class DynamicFOV : MonoBehaviour
{
    [Header("Ustawienia FOV")]
    [Tooltip("Referencja do obiektu Cinemachine Virtual Camera")]
    public CinemachineVirtualCamera virtualCamera;

    [Tooltip("Domy�lne pole widzenia (FOV) kamery")]
    public float defaultFOV = 60f;

    [Tooltip("FOV kamery, gdy spacja jest wci�ni�ta")]
    public float zoomedInFOV = 30f;

    [Tooltip("Pr�dko�� zmiany FOV")]
    public float fovChangeSpeed = 2f;

    // Warto�� docelowa dla FOV
    private float targetFOV;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Brak referencji do Cinemachine Virtual Camera!");
            return;
        }

        // Ustawienie domy�lnego FOV
        virtualCamera.m_Lens.FieldOfView = defaultFOV;
        targetFOV = defaultFOV;

        // Upewnij si�, �e kamera jest ustawiona na tryb perspektywiczny
        if (virtualCamera.m_Lens.Orthographic)
        {
            Debug.LogWarning("Kamera jest ustawiona na ortograficzn�. Funkcja FOV dzia�a tylko w trybie perspektywicznym.");
        }
    }

    void Update()
    {
        // Sprawd�, czy spacja jest wci�ni�ta
        if (Input.GetKey(KeyCode.Space))
        {
            // Ustaw docelowe FOV na warto�� przy zoomie
            targetFOV = zoomedInFOV;
        }
        else
        {
            // Je�li spacja nie jest wci�ni�ta, wr�� do domy�lnego FOV
            targetFOV = defaultFOV;
        }

        // P�ynnie interpoluj aktualne FOV do targetFOV
        float currentFOV = virtualCamera.m_Lens.FieldOfView;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * fovChangeSpeed);
    }
}
