using UnityEngine;

public class CollisionExitGame : MonoBehaviour
{
    // Ta metoda zostanie wywo�ana przy ka�dej kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Wykryto kolizj�, gra zostanie zako�czona.");

        // Zako�czenie aplikacji
        Application.Quit();
    }
}
