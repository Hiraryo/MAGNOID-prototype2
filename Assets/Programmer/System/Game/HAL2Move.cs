using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class HAL2Move : MonoBehaviour
{
    //Rigidbodyに変数を入れる
    private Rigidbody rb;
    private Vector3 moveVector;
    private float h, v;   //位置座標変数（x,z）
    public GameObject HAL2;

    //回転速度[]
    [SerializeField]
    private float rotationSpeed = 3.0f;
    //キャラクターコントローラー[]
    private CharacterController cCon;
    //キャラクターの速度
    private Vector3 velocity;
    //前の速度
    private Vector3 oldVelocity;
    //アニメーター
    private Animator animator;
    //歩行速度[]
    [SerializeField]
    private float moveSpeed = 2f;
    //ジャンプ力[]
    [SerializeField]
    private float jumpSpeed = 8.0f;

    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    //ハルの１フレームでの位置を保存する
    private Vector3 HAL2_Prev;

    void Start(){
        cCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        HAL2_Prev = transform.position; //ハルの位置を取得
    }

    void Update(){
        /*****
        移動処理
        *****/
        h = Input.GetAxis("Horizontal");    //左右矢印キーの値（-1.0~1.0)
        v = Input.GetAxis("Vertical");      //上下矢印キーの値（-1.0~1.0）

        if(cCon.isGrounded){    //キャラクターコントローラーのコライダが地面と接触しているかどうか
            gameObject.transform.Rotate(new Vector3(0,rotationSpeed * h, 0));
            moveDirection = moveSpeed * v * gameObject.transform.forward;
            if(Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
            }
        }
        if(moveDirection.magnitude > 0.01f){
                animator.SetFloat("Speed", moveDirection.magnitude);
            }else{
                animator.SetFloat("Speed", 0f);
            }
        moveDirection.y -= gravity * Time.deltaTime;
        cCon.Move(moveDirection * Time.deltaTime);
    }
}