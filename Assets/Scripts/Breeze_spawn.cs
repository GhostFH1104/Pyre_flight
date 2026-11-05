using UnityEngine;

public class Breeze_spawn : MonoBehaviour
{
    public GameObject breezePrefab;
    public float spawnrate = 4;
    public float timer = 0;
    public float height = 10;
    void Start()
    {
        Spawnbreeze();
    }

    void Update()
    {
        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawnbreeze();
            timer = 0;
        }
    }

    void Spawnbreeze()
    {
        float lowpoint = transform.position.y - height / 2;
        float highpoint = transform.position.y + height / 2;

        Instantiate(breezePrefab, new Vector3(transform.position.x, Random.Range(lowpoint, highpoint), 0), transform.rotation);
    }
}