using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public SpriteRenderer head;
    public Sprite[] spriteArray;
    public AnimatorController[] controllerArray;

    Animator animator;
    Rigidbody2D rb;
    CapsuleCollider2D hitbox;
    bool isMagnetising;
    bool isDead;
    bool isBlue;

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
        if (!isDead) {
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
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                animator.SetBool("isDead", true);
                animator.SetTrigger("death");
                isDead = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                isBlue = true;
                if (isMagnetising) {
                    head.sprite = spriteArray[0];
                } else {
                    head.sprite = spriteArray[1];
                }
                animator.runtimeAnimatorController = controllerArray[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                isBlue = false;
                if (isMagnetising) {
                    head.sprite = spriteArray[2];
                } else {
                    head.sprite = spriteArray[3];
                }
                animator.runtimeAnimatorController = controllerArray[1];
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
    }

    void stopMagnet() {
        animator.SetTrigger("stopMagnetising");
        animator.ResetTrigger("startMagnetising");
        isMagnetising = false;
        rb.freezeRotation = true;
        rb.rotation = 0f;
    }
}
