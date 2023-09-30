using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private int pointsToAdd = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Character player = other.GetComponent<Character>();

            if (player != null)
            {
                player.CollectItem(pointsToAdd);
            }

            Destroy(gameObject);
        }
    }
}
