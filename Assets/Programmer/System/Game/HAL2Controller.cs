//このプログラムは、主人公(HAL2)の処理を記述したものです。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RigidBodyコンポーネントを自動的に追加
[RequireComponent(typeof(Rigidbody))]

public class HAL2Controller : MonoBehaviour
{
    //歩行速度
    [SerializeField] private float _walkSpeed = 5.0f;
    //走行速度
    [SerializeField] private float _dashSpeed = 10.0f;
    //回転速度
    [SerializeField] private float _rotateSpeed = 120.0f;
    //ジャンプ力
    [SerializeField] private float _jumpPower = 400.0f;
    //浮遊時間
    private float _flyingTime = 0.0f;
    //移動速度
    private float _moveSpeed;
    //水平方向と鉛直方向の入力状態を代入（h : 水平, v : 鉛直）
    private float _h,_v;
    //ジャンプキー(今回はスペース)を押し続けた時間を測るためのフラグ（true : 押された, false : 離した）
    private bool _JumpTrigger = false;
    //地面に足がついているかを判定するフラグ（true : 接地している, false : 接地していない）
    private bool _Grounded = true;
    //移動する為の方向のベクトルを代入する変数
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody _myrigid;
    private Animator _animator;
    void Start()
    {
        _myrigid = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        InputGetKey();
        JumpMethod();
        MoveMethod();
    }

    //キーボードの入力受付
    void InputGetKey()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");
    }

    //ジャンプ処理
    void JumpMethod()
    {
        //浮遊時間の計測
        if(_JumpTrigger == true)
        {
            _flyingTime += 0.01f;
        }
    }

    //移動処理
    void MoveMethod()
    {
        //移動速度の判定
        _moveSpeed = (Input.GetKey(KeyCode.LeftShift)) ? _dashSpeed : _walkSpeed;

        //移動
        transform.Translate(0,0,_v * _moveSpeed * Time.fixedDeltaTime);

        //回転
        transform.Rotate(0, _h * _rotateSpeed * Time.fixedDeltaTime,0);

        _moveDirection = _v * gameObject.transform.forward;
        _JumpTrigger = (_Grounded) ? false : true;
        if(_Grounded == true)
        {
            if(_moveDirection.magnitude > 0.1f)
            {
                _animator.SetFloat("Speed",_moveDirection.magnitude);
            }
            else
            {
                _animator.SetFloat("Speed", 0f);
                _animator.SetBool("Jump",false);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _Grounded = false;
                _myrigid.AddForce(Vector3.up * _jumpPower);
                _animator.SetBool("Jump",true);
            }
        }
        Debug.Log("_Grounded : " + _Grounded + ", moveSpeed : " + _moveSpeed + ", _v : " + _v);
    }　　　

    //接地判定
    void OnCollisionEnter(Collision HAL2)
    {
        if(HAL2.gameObject.tag == "Ground")
        {
            _Grounded = true;
        }
    }
}