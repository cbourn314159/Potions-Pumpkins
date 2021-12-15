using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject spawnedObject;
    public float time;
    public float respawnTimer;
    public float respawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        time = 0;
        respawnDelay = 5;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (spawnedObject != null)
        {
            respawnTimer = time;
        }
        else
        {
            if(time - respawnTimer >= respawnDelay)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        spawnObject.transform.position = this.transform.position;
        spawnedObject = Instantiate(spawnObject);
    }
}
