using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraTriggerBehaviour : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 newCameraPosition;
    public float newCameraSize;
    public GameObject alternateTrigger;
    public Light2D[] lightsToTurnOff;
    public Light2D[] lightsToTurnOn;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            mainCamera.GetComponent<CameraBehaviour>().SetTargetPosition(newCameraPosition);
            mainCamera.GetComponent<CameraBehaviour>().SetTargetSize(newCameraSize);
            foreach (Light2D light in lightsToTurnOff) {
                light.intensity = 0f;
            }
            foreach (Light2D light in lightsToTurnOn) {
                light.intensity = 1f;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            alternateTrigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
