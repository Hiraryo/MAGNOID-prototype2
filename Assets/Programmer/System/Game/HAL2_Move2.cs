//このプログラムは、CharacterControllerを使った移動＋キーを押し続けた時間に応じてジャンプ力が変わる移動方法です。(RigidBody:あり)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //CharacterControllerコンポーネントを自動的に追加
[RequireComponent(typeof(Rigidbody))]           //RigidBodyコンポーネントを自動的に追加

public class HAL2_Move2 : MonoBehaviour
{
    [SerializeField] private float move_Speed = 3.0f;   //移動速度
    [SerializeField] private float rotate_Speed = 3.0f; //回転速度
    [SerializeField] private float jumping_time = 0.0f; //浮遊時間
    private bool JUMP_Trigger = false;                  //ジャンプキー(今回はスペース)を押し続けた時間を測るためのフラグ（押されたら : true, 離したら : false）
    private CharacterController controller;
    private Rigidbody rb;
    

    void Start(){
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        //押してから離すまでの時間でジャンプ力を計測する
        if(Input.GetButtonDown("Jump")){
            JUMP_Trigger = true;
        }
        if(Input.GetButtonUp("Jump")){
            JUMP_Trigger = false;
        }
    }

    void FixedUpdate(){
        //浮遊時間の計測
        if(JUMP_Trigger == true){
            jumping_time += 0.01f;
        }
        // Rotation
        transform.Rotate(0,Input.GetAxis("Horizontal") * rotate_Speed, 0);

        // Character direction in local space
        Vector3 forward = transform.transform.forward;

        float curSpeed = move_Speed * Input.GetAxis("Vertical");

        // Move with SimpleMove function
        controller.SimpleMove(forward * curSpeed);
    }
}
