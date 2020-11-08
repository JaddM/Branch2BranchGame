using UnityEngine;

// Author Jadd, Nov 8 2020

public class ToSpawn : MonoBehaviour {

    public Transform[] spawnsZones;
    public GameObject blockPrefab;

    // rate/trigger is the spawn rate of blocks
    private float rate = 0.8f;
    private float trigger = 0.0f;

    // objrate/trigger is the movement of the spawner so 
    // the spawner starts at the bottom and ascends so that blocks reach the ground faster
    private  float objRate = 1.0f;
    private float objTrigger = 0.0f;

    void Update() {

        if (Time.time > objTrigger) {

            objTrigger += objRate;
            if (transform.position.y < 145) {   // spawner ascends til y = 145
                transform.position = new Vector2(transform.position.x, transform.position.y + 5);
                
            }
            else {rate = 2.0f;} // slower rate due to static position of spawner
        }

        if (Time.time > trigger)
        {

            trigger += rate;
            Spawn();
            Spawn();
        }
    }

    void Spawn() {

        // To spawn random blocks
        int randomZone = Random.Range(0, spawnsZones.Length + 2);
        for (int i = 0; i < spawnsZones.Length; i++)
            if (randomZone == i) { GameObject block = Instantiate(blockPrefab, spawnsZones[i].position, Quaternion.identity); }
 
    }
}
