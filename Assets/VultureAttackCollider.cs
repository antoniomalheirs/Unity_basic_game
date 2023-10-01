using UnityEngine;

public class VultureAttackCollider : MonoBehaviour
{
    private Vulture vulture; // Refer�ncia para o script Vulture

    private void Start()
    {
        vulture = transform.parent.GetComponent<Vulture>(); // Obt�m a refer�ncia do componente pai
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character player = collision.GetComponent<Character>();

            if (player != null)
            {
                player.TakeDamage(9);
            }

        }
    }
}
