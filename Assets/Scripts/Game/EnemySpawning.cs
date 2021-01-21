using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public float lowerTimeToSpawn = 2f;
    public float upperTimeToSpawn = 4f;
    float nextSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + Random.Range(lowerTimeToSpawn, upperTimeToSpawn);
            randX = Random.Range(-1.9f, 0.9f);
            whereToSpawn = new Vector2(-0.5f, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
