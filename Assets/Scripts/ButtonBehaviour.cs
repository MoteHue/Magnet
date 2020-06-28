using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public AudioSource buttonAudio;
    Animator animator;
    public bool isPressed;
    public DoorBehaviour[] doors;

    private void Start() {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        buttonAudio.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        animator.SetBool("isPressed", true);
        isPressed = true;
        foreach (DoorBehaviour door in doors) {
            door.open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        buttonAudio.enabled = false;
        isPressed = false;
        animator.SetBool("isPressed", false);
        foreach (DoorBehaviour door in doors) {
            door.close();
        }
    }

}
