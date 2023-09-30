using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Character player = collision.GetComponent<Character>();

            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
