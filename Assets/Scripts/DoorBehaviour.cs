using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool isOpen = false;

    private void Update() {
        if (isOpen) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, 0.8f, transform.localPosition.z), 0.5f);
        } else {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, -3f, transform.localPosition.z), 0.5f);
        }
    }

    public void open() {
        isOpen = true;
    }

    public void close() {
        isOpen = false;
    }

}
