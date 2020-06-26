using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float cameraSmoothSpeed = 0.1f;
    Vector3 targetPosition;

    private void Start() {
        targetPosition = new Vector3(0f, 0f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothSpeed);
    }

    public void SetTargetPosition (Vector3 target) {
        targetPosition = target;
    }

}
