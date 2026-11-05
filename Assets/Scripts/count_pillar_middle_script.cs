using UnityEngine;

public class count_pillar_middle_script : MonoBehaviour
{
    private Counter_script counter;
    private bool hasScored = false;

    private void Start()
    {
        GameObject counterObj = GameObject.FindGameObjectWithTag("Counter_logic");
        if (counterObj != null)
        {
            counter = counterObj.GetComponent<Counter_script>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird") && !hasScored)
        {
            hasScored = true;
            if (counter != null)
            {
                counter.addscore();
            }
        }
    }
}