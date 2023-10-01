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

        if (collision.CompareTag("EnemyWOMAN"))
        {
            Woman enemy = collision.GetComponent<Woman>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyBOY"))
        {
            Boy enemy = collision.GetComponent<Boy>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyGIRL"))
        {
            Girl enemy = collision.GetComponent<Girl>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyDECEASED"))
        {
            Deceased enemy = collision.GetComponent<Deceased>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyMUMMY"))
        {
            Mummy enemy = collision.GetComponent<Mummy>();
            enemy.TakeDamage(2);
        }

        if (collision.CompareTag("EnemyVULTURE"))
        {
            Vulture enemy = collision.GetComponent<Vulture>();
            enemy.TakeDamage(2);
        }
    }
}
