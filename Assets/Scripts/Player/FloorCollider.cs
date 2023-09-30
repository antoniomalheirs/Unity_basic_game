using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorCollider : MonoBehaviour
{
    public bool canJump;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            canJump = true;
        }

        if (collision.CompareTag("Portal"))
        {
            SceneManager.LoadScene("Fase1");
        }
    }
}
