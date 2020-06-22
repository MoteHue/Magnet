using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    CapsuleCollider2D hitbox;
    bool isMagnetising;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementInput = Input.GetAxis("Horizontal");
        if (movementInput == 0) {
            animator.SetBool("isRunning", false);
        } else {
            animator.SetBool("isRunning", true);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            stopMagnet();
            animator.SetTrigger("takeOff");
            rb.AddRelativeForce(new Vector2(0f, 300f));
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (isMagnetising) {
                stopMagnet();
            } else {
                startMagnet();
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetBool("isJumping", false);
    }
    private void OnCollisionExit2D(Collision2D collision) {
        animator.SetBool("isJumping", true);
    }

    void startMagnet() {
        animator.SetTrigger("startMagnetising");
        animator.ResetTrigger("stopMagnetising");
        isMagnetising = true;
        rb.freezeRotation = false;
        hitbox.size = new Vector2(hitbox.size.x, 7f);
    }

    void stopMagnet() {
        animator.SetTrigger("stopMagnetising");
        animator.ResetTrigger("startMagnetising");
        isMagnetising = false;
        rb.freezeRotation = true;
        rb.rotation = 0f;
        hitbox.size = new Vector2(hitbox.size.x, 8f);
    }
}
