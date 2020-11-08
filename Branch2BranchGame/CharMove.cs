using UnityEngine;

// Author Jadd, Nov 8 2020

public class CharMove : MonoBehaviour {

    private Rigidbody2D charRigid;
    private Animator charAnimator;
    public GameObject starPrefab;
    public AudioClip char_gust;

    private bool onSurface;
    public Transform surfaceCheck;
    public LayerMask surfaceMask;

    // Character movement attributes
    private int jumpCounter;
    private const int jumpForce = 20;
    private const int horzIntensity = 15;
    private const int teleportDis = 5;

    // Teleport cooldown
    private int teleport = 5;
    private const float teleRate = 3.0f;
    private float teleTrigger = 0.0f;

    void Start() {

        charRigid = GetComponent<Rigidbody2D>();
        charAnimator = GetComponent<Animator>();
    }

    // To apply double jump and trigger animation
    void Update() {

        if (Time.time > teleTrigger) {

            teleTrigger += teleRate;
            if(teleport < 5) teleport++;    // Telport cooldown
        }

        if (onSurface) jumpCounter = 1;

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCounter > 0) {

            AudioSource.PlayClipAtPoint(char_gust, transform.position, 1);
            charAnimator.SetTrigger("jumpTrig");

            charRigid.velocity = Vector2.up * jumpForce;
            jumpCounter--;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && jumpCounter == 0 && onSurface) {  // Trigger double jump

            AudioSource.PlayClipAtPoint(char_gust, transform.position, 1);
            charAnimator.SetTrigger("jumpTrig");
            charRigid.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && charRigid.velocity.y > 0 && teleport > 0) {  // trigger teleport upwards

            AudioSource.PlayClipAtPoint(char_gust, transform.position, 1);
            teleport--;

            GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity); // is to mark last position before teleport
            Destroy(star, 2.0f);
            transform.position = new Vector2(transform.position.x, transform.position.y + teleportDis);
        }
    }

    // To invoke movement limits, trigger animation and position teleport location
    void FixedUpdate() {

        float charDirect = Input.GetAxisRaw("Horizontal");
        onSurface = Physics2D.OverlapCircle(surfaceCheck.position, 0.2f, surfaceMask);  // boolean to determine whether character is grounded
                                                                                        // to reset double jump

        if (charDirect > 0) {   // To determine teleport moving right

            charAnimator.SetTrigger("moveTrig");
            if (transform.position.x >= 11) charDirect = 0; // character limits

            if (Input.GetKeyDown(KeyCode.Space) && teleport > 0) {

                teleport--;

                GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity); // is to mark last position before teleport
                Destroy(star, 2.0f);

                float xCoord = 11f;
                if (transform.position.x + teleportDis < 11) { xCoord = transform.position.x + teleportDis; }   // To calculate teleport moving right

                transform.position = new Vector2(xCoord, transform.position.y);
            }

        }
        else if (charDirect < 0) {   // To determine teleport moving left

            charAnimator.SetTrigger("invMoveTrig");
            if (transform.position.x <= -11) charDirect = 0;    // character limits

            if (Input.GetKeyDown(KeyCode.Space) && teleport > 0) {

                teleport--;

                GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity); // is to mark last position before teleport
                Destroy(star, 2.0f);

                float xCoord = -11f;
                if (transform.position.x - teleportDis > -11) { xCoord = transform.position.x - teleportDis; }  // To calculate teleport moving left

                transform.position = new Vector2(xCoord, transform.position.y);
            }
        }

        charRigid.velocity = new Vector2(charDirect * horzIntensity, charRigid.velocity.y);
        if (charRigid.velocity.y < 0 && !onSurface) charAnimator.SetTrigger("fall");
    }
}
