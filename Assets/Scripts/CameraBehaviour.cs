using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float cameraSmoothSpeed = 0.1f;
    Vector3 targetPosition;
    float targetSize;

    private void Start() {
        transform.position = new Vector3(-1.9f, 0f, -10f);
        targetPosition = transform.position;
        GetComponent<Camera>().orthographicSize = 13.5f;
        targetSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothSpeed);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetSize, cameraSmoothSpeed);
    }

    public void SetTargetPosition (Vector3 target) {
        targetPosition = target;
    }

    public void SetTargetSize(float target) {
        targetSize = target;
    }

}
