using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackCollider : MonoBehaviour
{ 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyCOW"))
        {
            collision.GetComponent<Cow>().life--;
        }

        if (collision.CompareTag("EnemyCHICKEN"))
        {
            collision.GetComponent<Chicken>().life--;
        }

        if (collision.CompareTag("EnemySLIME"))
        {
            collision.GetComponent<Slime>().life--;
        }

        if (collision.CompareTag("EnemyGOBLIN"))
        {
            collision.GetComponent<Goblin>().life--;
        }

        if (collision.CompareTag("EnemyOLDMAN"))
        {
            OldMan enemy = collision.GetComponent<OldMan>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyOLDWOMAN"))
        {
            OldWoman enemy = collision.GetComponent<OldWoman>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyMAN"))
        {
            Man enemy = collision.GetComponent<Man>();
            enemy.TakeDamage(2);
        }
    }
}
