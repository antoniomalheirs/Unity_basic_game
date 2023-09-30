using UnityEngine;

public class Chicken : MonoBehaviour
{
    public int life = 5;
    public float velocidade = 0.35f;
    public Transform skin;
    public Transform pointA;
    public Transform pointB;

    public GameObject drop;
    private bool itemDropped = false;
    private Vector3 destino;
    private bool indoParaPontoB = true;

    private void Start()
    {
        destino = pointB.position;
    }

    private void Update()
    {
        if (life <= 0 && !itemDropped)
        {
            skin.GetComponent<Animator>().Play("Die", -1);
            Invoke("DestroyChicken", 0.75f);
            Invoke("DropItem", 0.75f);
            itemDropped = true;
            return;
        }

        if (life > 0)
        {
            Vector3 direcao = (destino - transform.position).normalized;

            transform.position += direcao * velocidade * Time.deltaTime;

            if (direcao.x < 0)
            {
                skin.localScale = new Vector3(-1, 1, 1);
            }
            else if (direcao.x > 0)
            {
                skin.localScale = new Vector3(1, 1, 1);
            }

            if (Vector3.Distance(transform.position, destino) < 0.2f)
            {
                indoParaPontoB = !indoParaPontoB;
                destino = indoParaPontoB ? pointB.position : pointA.position;
            }
        }
    }

    private void DestroyChicken()
    {
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
