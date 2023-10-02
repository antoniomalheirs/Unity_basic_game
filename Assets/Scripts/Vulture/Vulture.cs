using UnityEngine;

public class Vulture : MonoBehaviour
{
    public int life = 5;
    public float velocidade = 2f;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public float tempoDeEsperaNoPontoC = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isMoving = true;
    private Vector3 destino;
    private enum Destino { A, B, C }
    private Destino destinoAtual = Destino.B;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        destino = pointB.position;
    }

    private void Update()
    {
        if (life <= 0)
        {
            animator.Play("Die", -1);
            Invoke(nameof(DestroyVulture), 1.5f);
            return;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveVulture();
        }
    }

    private void MoveVulture()
    {
        Vector3 direcao = (destino - transform.position).normalized;
        rb.velocity = direcao * velocidade;

        // Invertendo o sprite horizontalmente conforme a direção do movimento
        if (direcao.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direcao.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Vector3.Distance(transform.position, destino) < 0.3f)
        {
            if (destinoAtual == Destino.C)
            {
                animator.Play("Idle", -1);
                isMoving = false;
                Invoke(nameof(ChangeDestination), tempoDeEsperaNoPontoC);
            }
            else
            {
                animator.Play("Fly", -1);
                ChangeDestination();
            }
        }
    }

    private void ChangeDestination()
    {
        switch (destinoAtual)
        {
            case Destino.A:
                destinoAtual = Destino.B;
                destino = pointB.position;
                break;
            case Destino.B:
                destinoAtual = Destino.C;
                destino = pointC.position;
                break;
            case Destino.C:
                destinoAtual = Destino.A;
                destino = pointA.position;
                break;
        }
        animator.Play("Fly", -1);
        isMoving = true;
    }

    public void TakeDamage(int damageAmount)
    {
        animator.Play("Hurt", -1);
        life -= damageAmount;
    }

    private void DestroyVulture()
    {
        Destroy(gameObject);
    }
}
