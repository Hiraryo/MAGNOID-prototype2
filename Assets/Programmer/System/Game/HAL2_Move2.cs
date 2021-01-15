using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HAL2_Move2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;

    private CharacterController controller;

    void Start(){
        // Get the component
        controller = GetComponent<CharacterController>();
    }

    void Update(){
        // Rotation
        transform.Rotate(0,Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Character direction in local space
        Vector3 forward = transform.transform.forward;

        float curSpeed = speed * Input.GetAxis("Vertical");

        // Move with SimpleMove function
        controller.SimpleMove(forward * curSpeed);
    }
}
