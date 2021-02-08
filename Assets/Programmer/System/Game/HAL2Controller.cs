//このプログラムは、Rigidbodyを使った移動プログラムです。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]                       //RigidBodyコンポーネントを自動的に追加

public class HAL2Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;       //移動速度
    [SerializeField] private float _rotateSpeed = 120.0f;   //回転速度
    private float _flyingTime = 0.0f;                       //浮遊時間
    private float _jumpPower = 400.0f;                      //ジャンプ力
    private float _h,_v;                                    //水平方向と垂直方向の入力状態を代入（h : 水平, v : 垂直）
    private bool _JumpTrigger = false;                      //ジャンプキー(今回はスペース)を押し続けた時間を測るためのフラグ（true : 押された, false : 離した）
    private bool _Grounded;                                 //地面に足がついているかを判定するフラグ（true : 接地している, false : 接地していない）
    private Vector3 _moveDirection = Vector3.zero;          //移動する為の方向のベクトルを代入する変数
    private Rigidbody _myrigid;
    private Animator _animator;

    void Start()
    {
        _myrigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump"))
        {
            _JumpTrigger = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            _JumpTrigger = false;
        }
    }
    void FixedUpdate()
    {
        //浮遊時間の計測
        if(_JumpTrigger == true)
        {
            _flyingTime += 0.01f;
        }
        //移動処理
        Vector3 _velocity = new Vector3(0,0,_v);
        _velocity = transform.TransformDirection(_velocity);  //ローカル空間からワールド空間へVectorを変換
        //Rigidbody + AddForceを使った移動方法をここに書く
        //
        //

        transform.localPosition += _velocity * _moveSpeed * Time.fixedDeltaTime;

        //回転処理
        transform.Rotate(0, _h * _rotateSpeed * Time.fixedDeltaTime, 0);

        _moveDirection = _moveSpeed * _v * gameObject.transform.forward;
        if(_Grounded == true)
        {
            if(_moveDirection.magnitude > 0.1f)
            {
                _animator.SetFloat("Speed",_moveDirection.magnitude);
            }
            else
            {
                _animator.SetFloat("Speed", 0f);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _Grounded = false;
                _myrigid.AddForce(Vector3.up * _jumpPower);
            }
        }
        //Debug.Log("moveDirection : " + _moveDirection + ", Position : " + transform.position + ", Rotation : " + transform.rotation);
    }

    void OnTriggerEnter(Collider HAL2)
    {
        if(HAL2.gameObject.tag == "Ground")
        {
            _Grounded = true;
        }
    }
}
