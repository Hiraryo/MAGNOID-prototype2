using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAL2_Controller : MonoBehaviour
{
    public float Moving_speed = 5f;
    public float Rotate_speed = 120f;
    public float Jump_power = 400.0f;
    private bool Grounded;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(0,0,v);

        // Convert character orientation to orientation in local space.
        velocity = transform.TransformDirection(velocity);

        // Character move
        transform.localPosition += velocity * Moving_speed * Time.fixedDeltaTime;

        // Character rotation
        transform.Rotate(0, h * Rotate_speed * Time.fixedDeltaTime, 0);

        moveDirection = Moving_speed * v * gameObject.transform.forward;
        if(Grounded == true){
            if(moveDirection.magnitude > 0.1f){
                animator.SetFloat("Speed",moveDirection.magnitude);
            }else{
                animator.SetFloat("Speed", 0f);
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                Grounded = false;
                rb.AddForce(Vector3.up * Jump_power);
            }
        }
        Debug.Log("moveDirection : " + moveDirection + ", Position : " + transform.position + ", Rotation : " + transform.rotation);
    }

    void OnTriggerEnter(Collider HAL2){
        if(HAL2.gameObject.tag == "Ground"){
            Grounded = true;
        }
    }
}
