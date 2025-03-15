using UnityEngine;

public class CollisionExitGame : MonoBehaviour
{
    // Ta metoda zostanie wywo³ana przy ka¿dej kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Wykryto kolizjê, gra zostanie zakoñczona.");

        // Zakoñczenie aplikacji
        Application.Quit();
    }
}
