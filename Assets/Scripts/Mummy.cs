using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    public int life = 5;
    public float velocidade = 0.65f;
    public Transform skin;
    public Transform pointA;
    public Transform pointB;
    public Collider2D areaDeAtivacao;
    public GameObject drop;
    private Rigidbody2D rb;

    private Vector3 destino;
    private bool indoParaPontoB = true;
    private bool itemDropped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destino = pointB.position;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida do inimigo
        skin.GetComponent<Animator>().Play("Hurt", -1);
        life -= damageAmount;
    }

    private void Update()
    {
        if (life <= 0 /*&& !itemDropped*/)
        {
            skin.GetComponent<Animator>().Play("Die", -1);
            Invoke("DestruirOldMan", 1.5f);
            //Invoke("DropItem", 2.0f);
            itemDropped = true;
            return;
        }

        if (life > 0)
        {
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

            if (Vector3.Distance(transform.position, destino) < 0.3f)
            {
                indoParaPontoB = !indoParaPontoB;
                destino = indoParaPontoB ? pointB.position : pointA.position;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 direcao = (destino - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
        // Adicione a força aqui
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            skin.GetComponent<Animator>().Play("Attack", -1);
        }
    }

    private void DropItem()
    {
        if (drop != null)
        {
            //Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    private void DestruirOldMan()
    {
        Destroy(gameObject);
    }
}
