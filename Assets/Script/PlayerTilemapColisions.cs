using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollision : MonoBehaviour
{
    // Referencja do Tilemapy, z któr¹ mamy sprawdzaæ kolizje
    public Tilemap tilemap;

    // Metoda wywo³ywana przy kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zak³adamy, ¿e kolizja dotyczy Tilemapy, 
        // ale mo¿esz dodaæ warunki sprawdzaj¹ce nazwê lub tag
        if (collision.collider.gameObject == tilemap.gameObject)
        {
            // Pobranie pierwszego punktu kontaktu
            ContactPoint2D contact = collision.GetContact(0);
            Vector3 contactPoint = contact.point;

            // Przekszta³cenie pozycji punktu kolizji z przestrzeni œwiata do wspó³rzêdnych siatki
            Vector3Int cellPosition = tilemap.WorldToCell(contactPoint);

            // Pobranie kafelka z danej pozycji
            TileBase collidedTile = tilemap.GetTile(cellPosition);

            if (collidedTile != null)
            {
                Debug.Log("Gracz uderzy³ w kafelek: " + collidedTile.name + " na pozycji: " + cellPosition);
            }
            else
            {
                Debug.Log("Na pozycji " + cellPosition + " nie znaleziono kafelka");
            }
        }
    }

}
