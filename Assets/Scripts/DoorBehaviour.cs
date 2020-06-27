using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void open() {
        animator.SetBool("isOpen", true);
    }

    public void close() {
        animator.SetBool("isOpen", false);
    }

}
