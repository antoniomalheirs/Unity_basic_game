using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform skin;
    public GameObject itemToDropPrefab;
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen == false)
        {
            if (collision != null)
            {
                isOpen = true;
                skin.GetComponent<Animator>().Play("Open", -1);
            }
            Invoke("DropItem", 0.75f);
        }
    }

    private void DropItem()
    {
        if (itemToDropPrefab != null)
        {
            Instantiate(itemToDropPrefab, transform.position, Quaternion.identity);
        }
    }
}

