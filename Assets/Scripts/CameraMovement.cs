using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float fastSpeed;
    public float slowSpeed;
    private float cameraSpeed;
    void Update()
    {
        cameraSpeed = Input.GetKey(KeyCode.LeftShift)?fastSpeed : slowSpeed;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, vertical, 0);
        direction.Normalize();
        transform.position += direction * cameraSpeed * Time.deltaTime;
    }
}
