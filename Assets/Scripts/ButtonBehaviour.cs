using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ButtonBehaviour : MonoBehaviour
{
    Animator animator;
    public bool isPressed;
    public DoorBehaviour[] doors;

    private void Start() {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        foreach (DoorBehaviour door in doors) {
            door.open();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        animator.SetBool("isPressed", true);
        isPressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isPressed = false;
        animator.SetBool("isPressed", false);
        foreach (DoorBehaviour door in doors) {
            door.close();
        }
    }

}
