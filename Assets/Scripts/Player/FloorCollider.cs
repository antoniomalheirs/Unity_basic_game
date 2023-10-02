using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorCollider : MonoBehaviour
{
    public bool canJump;
    //public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform parentTransform = transform.parent;

        PlayerController player = parentTransform.GetComponent<PlayerController>();


        if (collision.CompareTag("Floor"))
        {
            canJump = true;
            //player.SetSpeed(3f);
            player.GetComponent<PlayerController>().speed = 3;
        }

        if (collision.CompareTag("Water"))
        {
            // Modificar a variável speed no script PlayerController usando o método SetSpeed
            //player.SetSpeed(0.65f);
            player.GetComponent<PlayerController>().speed = 0.65f;
        }


        if (collision.CompareTag("Portal"))
        {
            // ...
        }
    }

}
