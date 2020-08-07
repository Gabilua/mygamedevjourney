using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Camera cam;
    [SerializeField] float speed, zoomSpeed;
    [SerializeField] Vector2 zoomLimits;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize + zoomSpeed * Input.GetAxis("Mouse ScrollWheel"), Time.deltaTime);

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomLimits.x, zoomLimits.y);
    }
}
