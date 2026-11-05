using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Pillar_spawn_script : MonoBehaviour
{
    public GameObject pillarPrefab;
    public float spawnrate = 2f;
    public float timer = 0;
    public float height = 10;
    public float collect = 1;

    public Bird_script bird;
    public Counter_script counter;
    public GameObject portal;
    public bool spawningStopped = false;
    public bool portalSpawned = false;

    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("Counter_logic").GetComponent<Counter_script>();
        portal.SetActive(false);
        Spawnpillar();
    }

    void Update()
    {
        if (counter.coin >= collect)
        {
            spawningStopped = true;
        }

        if (spawningStopped)
        {
            if (GameObject.FindGameObjectsWithTag("pillar").Length == 0 && !portalSpawned)
            {
                portal.SetActive(true);
                portalSpawned = true;
            }

            return;
        }

        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawnpillar();
            timer = 0;
        }
    }

    void Spawnpillar()
    {
        float lowestpoint = transform.position.y - height / 2;
        float highestpoint = transform.position.y + height / 2;

        GameObject newPillar = Instantiate(pillarPrefab, new Vector3(transform.position.x, Random.Range(lowestpoint, highestpoint), 0), transform.rotation);

        if (Random.Range(1, 6) != 1)
        {
            foreach (Transform child in newPillar.GetComponentsInChildren<Transform>(true))
            {
                if (child.CompareTag("coin"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}