using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Camera mainCamera;
    public float cameraSmoothSpeed = 0.5f;

    public SpriteRenderer head;
    public Sprite[] spriteArray;
    public AnimatorController[] controllerArray;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.9f;
    public Transform frontCheck;
    public float frontCheckRadius = 1f;

    public float moveSpeed = 10f;
    public float jumpHeight = 10f;

    public LayerMask crateMask;
    public LayerMask redMagnetMask;
    public LayerMask blueMagnetMask;
    public float magnetismRadius = 10f;

    public float magnetismStrength = 0.1f;

    public Canvas pauseCanvas;

    Animator animator;
    Rigidbody2D rb;
    bool isMagnetising;
    bool isDead;
    bool isBlue = true;

    bool isTouchingFront;
    bool isGrounded;
    bool facingRight = true;

    bool isGamePaused;

    Collider2D[] attractions;
    Collider2D[] repels;
    Collider2D[] crates;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.runtimeAnimatorController = controllerArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.gameObject.SetActive(true);
            pauseCanvas.GetComponentInChildren<PauseMenu>().PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            reloadScene();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, frontCheckRadius, groundMask);

        // Get nearby magnets
        if (isBlue) {
            attractions = Physics2D.OverlapCircleAll(transform.position, magnetismRadius, redMagnetMask);
            repels = Physics2D.OverlapCircleAll(transform.position, magnetismRadius, blueMagnetMask);
        } else {
            attractions = Physics2D.OverlapCircleAll(transform.position, magnetismRadius, blueMagnetMask);
            repels = Physics2D.OverlapCircleAll(transform.position, magnetismRadius, redMagnetMask);
        }

        if (isMagnetising) {
            foreach (Collider2D attraction in attractions) {
                Vector2 direction = attraction.transform.position - transform.position;
                rb.velocity += direction * magnetismStrength;
            }
            foreach (Collider2D repel in repels) {
                Vector2 direction = repel.transform.position - transform.position;
                rb.velocity -= direction * magnetismStrength;
            }
        } 

        if (!isGrounded) {
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isJumping", false);
        }

        if (!isDead) {
            float movementInput = Input.GetAxis("Horizontal");

            // Check to flip
            if (!facingRight && movementInput > 0 && !isMagnetising) {
                Flip();
            } else if (facingRight && movementInput < 0 && !isMagnetising) {
                Flip();
            }

            // Check for magnetism
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isMagnetising) {
                    StopMagnet();
                } else {
                    StartMagnet();
                }
            }

            // Check for death
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                animator.SetBool("isDead", true);
                animator.SetTrigger("death");
                isDead = true;
            }

            // Check for colour switch
            if (Input.GetKeyDown(KeyCode.Return)) {
                if (!isBlue) {
                    isBlue = true;
                    animator.runtimeAnimatorController = controllerArray[0];
                } else {
                    isBlue = false;
                    animator.runtimeAnimatorController = controllerArray[1];
                }
                if (isMagnetising) {
                    animator.ResetTrigger("startMagnetising");
                    animator.SetTrigger("startMagnetising");
                    animator.SetBool("isMagnetising", true);
                }
            }
        }

    }

    private void FixedUpdate() {
        if (!isDead) {
            float movementInput = Input.GetAxis("Horizontal");
            // Check for running
            if (movementInput == 0) {
                animator.SetBool("isRunning", false);
            } else if (!isMagnetising) {
                animator.SetBool("isRunning", true);
                if ((!isTouchingFront && !isGrounded) || (isGrounded)) {
                    rb.velocity = new Vector2(moveSpeed * movementInput, rb.velocity.y);
                }
            }

            // Check for jumping
            if (Input.GetAxis("Vertical") > 0 && isGrounded) {
                StopMagnet();
                animator.SetTrigger("takeOff");
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }

            // Inform nearby crates
            crates = Physics2D.OverlapCircleAll(transform.position, magnetismRadius, crateMask);
            foreach (Collider2D crate in crates) {
                crate.GetComponent<CrateBehaviour>().informMagnetised(isBlue, transform.position, magnetismStrength, isMagnetising);
            }

        }
    }

    void StartMagnet() {
        animator.SetTrigger("startMagnetising");
        animator.ResetTrigger("stopMagnetising");
        animator.SetBool("isMagnetising", true);
        isMagnetising = true;
        rb.freezeRotation = false;
    }

    void StopMagnet() {
        animator.SetTrigger("stopMagnetising");
        animator.ResetTrigger("startMagnetising");
        animator.SetBool("isMagnetising", false);
        isMagnetising = false;
        rb.freezeRotation = true;
        rb.rotation = 0f;
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x = -Scaler.x;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isDead && collision.gameObject.CompareTag("Death")) {
            rb.freezeRotation = false;
            animator.SetBool("isDead", true);
            animator.SetTrigger("death");
            isDead = true;
            Invoke("reloadScene", 2f);
        }

        if (collision.gameObject.CompareTag("Crate")) {
            rb.velocity = Vector2.zero;
        }

    }

    void reloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
