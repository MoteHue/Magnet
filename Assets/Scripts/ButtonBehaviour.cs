using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ButtonBehaviour : MonoBehaviour
{
    Animator animator;
    public bool isPressed;

    private void Start() {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        animator.SetBool("isPressed", true);
        isPressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isPressed = false;
        Invoke("Unpress", 0);
    }

    void Unpress() {
        if (!isPressed) {
            animator.SetBool("isPressed", false);
        }
    }


}
