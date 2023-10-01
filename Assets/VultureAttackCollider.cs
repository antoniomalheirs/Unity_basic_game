using UnityEngine;

public class VultureAttackCollider : MonoBehaviour
{
    private Vulture vulture; // Referência para o script Vulture

    private void Start()
    {
        vulture = transform.parent.GetComponent<Vulture>(); // Obtém a referência do componente pai
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
