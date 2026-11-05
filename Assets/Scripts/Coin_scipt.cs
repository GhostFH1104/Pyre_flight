using UnityEngine;

public class Coin_script : MonoBehaviour
{
    public Counter_script coin;

    private bool coincollected = false;

    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("Counter_logic").GetComponent<Counter_script>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird") && !coincollected)
        {
            coincollected = true;
            coin.addcoin();
            Destroy(gameObject);
        }
    }
}