using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 vel;
    public Transform floorCollider;
    public Transform skin;
    int ataque;
    private float dashTime;
    public int speed=3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dashTime = dashTime + Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && dashTime > 1)
        {
            dashTime = 0;

            skin.GetComponent<Animator>().Play("Dash", -1);

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 500, 0));
        }

        if (Input.GetButtonDown("Jump") && floorCollider.GetComponent<FloorCollider>().canJump == true)
        {
            skin.GetComponent<Animator>().Play("Jump", -1);
            rb.velocity = Vector2.zero;
            floorCollider.GetComponent<FloorCollider>().canJump = false;
            rb.AddForce(new Vector2(0, 400));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            skin.GetComponent<Animator>().Play("Attack", -1);
        }

        vel = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            skin.GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("Run", false);
        }

        //verificando se a variável life (script Character), é zero ou menor
        /*if (GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
        }*/
    }

    private void FixedUpdate()
    {
        if (dashTime >= 0.5f)
        {
            rb.velocity = vel;
        }
    }
}
