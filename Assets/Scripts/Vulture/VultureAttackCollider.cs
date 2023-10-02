using UnityEngine;

public class VultureAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            

            Character player = collision.GetComponent<Character>();

            if (player != null)
            {
                player.TakeDamage(9);
                Debug.Log("Ataque iniciado.");
            }

        }
    }
}
