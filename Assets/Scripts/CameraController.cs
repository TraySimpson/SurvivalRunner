using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private GameObject targetObject;
    private float yCoordinate;
    private float xOffset;

    private void Start()
    {
        yCoordinate = transform.position.y;
        xOffset = transform.position.z;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = targetObject.transform.position;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(targetPosition.x, yCoordinate, targetPosition.z + xOffset),
            ref velocity,
            smoothTime);
    }
}
