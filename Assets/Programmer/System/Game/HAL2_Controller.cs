using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAL2_Controller : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 120f;

    void FixedUpdate(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(0,0,v);

        // Convert character orientation to orientation in local space.
        velocity = transform.TransformDirection(velocity);

        // Character move
        transform.localPosition += velocity * speed * Time.fixedDeltaTime;

        // Character rotation
        transform.Rotate(0, h * rotateSpeed * Time.fixedDeltaTime, 0);
    }
}
