using UnityEngine;

public class Ladderlarge : MonoBehaviour
{
    private bool isOnLadder = false;
    private Rigidbody2D playerRb; // Refer�ncia para o Rigidbody2D do jogador
    private Collider2D objetoAcimaDaEscada;
    private Animator playerAnimator;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = true;
            playerRb = other.GetComponent<Rigidbody2D>(); // Obt�m a refer�ncia para o Rigidbody2D do jogador
            objetoAcimaDaEscada.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = false;
            playerRb = null; // Limpa a refer�ncia quando o jogador sai da escada
            objetoAcimaDaEscada.enabled = true;
            playerAnimator.SetBool("IsClimbing", false);
        }
    }

    void Start()
    {
        // Obter uma refer�ncia para o objeto acima da escada
        objetoAcimaDaEscada = GameObject.Find("floor0").GetComponent<Collider2D>();
        playerAnimator = GameObject.Find("PSkin").GetComponent<Animator>(); ;
    }

    void Update()
    {
        if (isOnLadder && playerRb != null)
        {
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.velocity = new Vector2(playerRb.velocity.x, verticalInput);

            // Atualiza o par�metro da anima��o
            playerAnimator.SetBool("IsClimbing", isOnLadder && Mathf.Abs(verticalInput) > 0.1f);
        }
    }

}
