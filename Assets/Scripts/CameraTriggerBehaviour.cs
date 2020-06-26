using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraTriggerBehaviour : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 newCameraPosition;
    public GameObject alternateTrigger;
    public Light2D[] lightsToTurnOff;
    public Light2D[] lightsToTurnOn;

    private void OnTriggerEnter2D(Collider2D collision) {
        mainCamera.GetComponent<CameraBehaviour>().SetTargetPosition(newCameraPosition);
        foreach (Light2D light in lightsToTurnOff) {
            light.intensity = 0f;
        }
        foreach (Light2D light in lightsToTurnOn) {
            light.intensity = 1f;
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        alternateTrigger.SetActive(true);
        gameObject.SetActive(false);
    }

}
