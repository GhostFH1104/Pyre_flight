using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    public Counter_script counter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            counter.Gameover();
        }
    }
}