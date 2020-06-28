using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public AudioSource doorAudio;
    bool isOpen = false;

    private void Update() {
        if (isOpen) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, 0.8f, transform.localPosition.z), 0.5f);
        } else {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, -3f, transform.localPosition.z), 0.5f);
        }
    }

    public void open() {
        doorAudio.enabled = true;
        isOpen = true;
        Invoke("disableAudio", 1f);
    }

    public void close() {
        doorAudio.enabled = true;
        isOpen = false;
        Invoke("disableAudio", 1f);
    }

    void disableAudio() {
        doorAudio.enabled = false;
    }

}
