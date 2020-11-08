using UnityEngine;

// Author Jadd, Nov 8 2020

public class BlockObj : MonoBehaviour {

    // To eliminate game object to avoid clutter
    void Update() {if(transform.position.y < 0) Destroy(gameObject);}
}
