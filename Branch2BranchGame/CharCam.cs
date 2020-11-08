using UnityEngine;

// Author Jadd, Nov 8 2020

public class CharCam : MonoBehaviour {

    // To fix camera to main character
    public Transform charPos;
    void Update() { transform.position = new Vector3(0, charPos.position.y + 3, -20); }
}
