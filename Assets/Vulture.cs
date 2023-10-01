using System.Collections;
using UnityEngine;

public class Vulture : MonoBehaviour
{
    public int life = 5;
    public float velocidade = 0.65f;
    public Transform skin;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public GameObject drop;
    private Rigidbody2D rb;
    private Animator animator;

    private enum Destino
    {
        A,
        B,
        C
    }

    private Destino destinoAtual = Destino.B;
    public float tempoDeEsperaNoPontoC = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = skin.GetComponent<Animator>();
        StartCoroutine(MoverParaDestino());
    }

    private IEnumerator MoverParaDestino()
    {
        while (true)
        {
            Vector3 destino = Vector3.zero;
            switch (destinoAtual)
            {
                case Destino.A:
                    destino = pointA.position;
                    break;
                case Destino.B:
                    destino = pointB.position;
                    break;
                case Destino.C:
                    destino = pointC.position;
                    break;
            }

            Vector3 direcao = (destino - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;

            if (direcao.x < 0)
            {
                skin.localScale = new Vector3(1, 1, 1);
            }
            else if (direcao.x > 0)
            {
                skin.localScale = new Vector3(-1, 1, 1);
            }

            if ((destinoAtual == Destino.A || destinoAtual == Destino.B))
            {
                //animator.Play("Fly", -1);
            }

            if (Vector3.Distance(transform.position, destino) < 0.2f)
            {
                if (destinoAtual == Destino.C)
                {
                    animator.Play("Idle", -1);
                    yield return new WaitForSeconds(tempoDeEsperaNoPontoC);
                }

                TrocarDestino();
            }

            yield return null;
        }
    }

    private void TrocarDestino()
    {
        switch (destinoAtual)
        {
            case Destino.A:
                destinoAtual = Destino.B;
                break;
            case Destino.B:
                destinoAtual = Destino.C;
                break;
            case Destino.C:
                destinoAtual = Destino.A;
                break;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        skin.GetComponent<Animator>().Play("Hurt", -1);
        life -= damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            skin.GetComponent<Animator>().Play("Attack", -1);
            return;
        }
    }

    private void DestruirOldMan()
    {
        Destroy(gameObject);
    }
}
