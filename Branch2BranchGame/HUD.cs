using UnityEngine;
using UnityEngine.UI;

// Author Jadd, Nov 8 2020

public class HUD : MonoBehaviour {

    public GameObject charPos;
    public Text text;

    // To count the number of teleports left
    private int teleport = 5;
    private const float teleRate = 3.0f;
    private float teleTrigger = 0.0f;

    // To illusatrate progress in the game
    void Update() {


        string info = "Score: " + Mathf.RoundToInt(charPos.GetComponent<Transform>().position.y).ToString() + "\n" + "Teleports: " + teleport.ToString();
        text.text = info;

        if (Time.time > teleTrigger) {

            teleTrigger += teleRate;
            if (teleport < 5) teleport++;
        }

        if (Input.GetKeyDown(KeyCode.Space) && charPos.GetComponent<Rigidbody2D>().velocity.y > 0 && teleport > 0) teleport--;
    }
}
