using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Character player = other.GetComponent<Character>();

            if (player != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
