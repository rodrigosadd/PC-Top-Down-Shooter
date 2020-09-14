using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera variables")]
    public Transform targetCamera;
    public float minPositionX;
    public float maxPositionX;
    public float minPositionY;
    public float maxPositionY;
    public float smoothness;

    void Update()
    {
        GetTarget();
    }

    void GetTarget()
    {
        Vector3 positionCam = new Vector3(targetCamera.position.x, targetCamera.position.y, transform.position.z);

        positionCam.x = Mathf.Clamp(positionCam.x, minPositionX, maxPositionX);
        positionCam.y = Mathf.Clamp(positionCam.y, minPositionY, maxPositionY);

        transform.position = Vector3.Lerp(transform.position, positionCam, Time.deltaTime * smoothness);
    }

}
