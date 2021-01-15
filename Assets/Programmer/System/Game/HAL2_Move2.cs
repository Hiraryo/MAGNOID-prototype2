using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAL2_Move2 : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20.0f;
    //キャラクターコントローラー
    private CharacterController controller;
    //アニメーター
    private Animator animator;
    private Vector3 targetDirection;
    
    private Vector3 currentRotateForward = Vector3.zero;
    private float rotate_speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //animation.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    protected void Rotate(){
        // Get current movement vector.
        Vector3 moveVelocity = controller.velocity;
        moveVelocity.y = 0;

        // If the movement vector is other than the zero vector, set it as the rotation vector.
        if(moveVelocity != Vector3.zero){
            currentRotateForward = moveVelocity;
        }

        // Get angle and direction of rotation.
        float value = Mathf.Min(1, rotate_speed * Time.deltaTime / Vector3.Angle(transform.forward, currentRotateForward));
        Vector3 newForward = Vector3.Slerp(transform.forward, currentRotateForward, value);

        if(newForward != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(newForward, transform.up);
        }
    }
/*
    void FixedUpdate(){
        moveDirection = Vector3.zero;
        moveDirection.y -= gravity;

        targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if(targetDirection.magnitude > 0.1f){
            transform.rotation = Quaternion.LookRotation(targetDirection);
            moveDirection += transform.forward*0.0001f;
            //animator.CrossFade("walk");
        }else{
            GetComponent<Animation>().CrossFade("idle");
        }
        //controller.Move(moveDirection*Time.deltaTime);
    }
    */
}
