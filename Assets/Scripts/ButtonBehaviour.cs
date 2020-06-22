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

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetTrigger("Pressed");
        isPressed = true;
    }


}
