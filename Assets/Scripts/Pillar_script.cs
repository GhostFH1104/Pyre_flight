using UnityEngine;

public class Pillar_script : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadzone = -20;
    void Start()
    {

    }

    void Update()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadzone)
        {
            Debug.Log("Pillar destroyed");
            Destroy(gameObject);
        }
    }
}