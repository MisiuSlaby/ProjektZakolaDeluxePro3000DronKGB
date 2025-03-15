using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollision : MonoBehaviour
{
    // Referencja do Tilemapy, z kt�r� mamy sprawdza� kolizje
    public Tilemap tilemap;

    // Metoda wywo�ywana przy kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zak�adamy, �e kolizja dotyczy Tilemapy, 
        // ale mo�esz doda� warunki sprawdzaj�ce nazw� lub tag
        if (collision.collider.gameObject == tilemap.gameObject)
        {
            // Pobranie pierwszego punktu kontaktu
            ContactPoint2D contact = collision.GetContact(0);
            Vector3 contactPoint = contact.point;

            // Przekszta�cenie pozycji punktu kolizji z przestrzeni �wiata do wsp�rz�dnych siatki
            Vector3Int cellPosition = tilemap.WorldToCell(contactPoint);

            // Pobranie kafelka z danej pozycji
            TileBase collidedTile = tilemap.GetTile(cellPosition);

            if (collidedTile != null)
            {
                Debug.Log("Gracz uderzy� w kafelek: " + collidedTile.name + " na pozycji: " + cellPosition);
            }
            else
            {
                Debug.Log("Na pozycji " + cellPosition + " nie znaleziono kafelka");
            }
        }
    }

}
