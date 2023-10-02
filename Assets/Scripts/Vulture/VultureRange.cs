using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureRange : MonoBehaviour
{
    public Transform skin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Colisão detectada com: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            skin.GetComponent<Animator>().Play("Attack", -1);
            // Adicione um Debug.Log aqui para verificar se a animação de ataque está sendo reproduzida corretamente.
            //Debug.Log("Ataque iniciado.");
        }
    }
}
