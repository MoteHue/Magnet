using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public AudioSource collisionAudio;

    public LayerMask playerMask;
    public float magnetismRadius;

    Rigidbody2D rb;
    public bool isBlue;
    bool isMagnetised;
    bool attracted;
    Vector2 targetPosition;
    float magnetismStrength;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Physics2D.OverlapCircle(transform.position, magnetismRadius, playerMask)) {
            isMagnetised = false;
        }

        Vector2 direction = targetPosition - new Vector2(transform.position.x, transform.position.y);

        if (isMagnetised) {
            if (attracted) {
                rb.velocity += direction * magnetismStrength;
            } else { 
                rb.velocity -= direction * magnetismStrength;
            }
        }

    }

    public void informMagnetised(bool blue, Vector2 playerPosition, float strength, bool magnetised) {
        isMagnetised = magnetised;
        if (isBlue) {
            attracted = !blue;
        } else {
            attracted = blue;
        }
        targetPosition = playerPosition;
        magnetismStrength = strength;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Player")) {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        } else {
            collisionAudio.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            collisionAudio.enabled = false;
        }
    }

}
