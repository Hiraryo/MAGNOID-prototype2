﻿//参考：http://sasanon.hatenablog.jp/entry/2017/09/17/041612
using UnityEngine;

// プレイヤー追従カメラ
public class PlayerFollowCamera : MonoBehaviour {
    [SerializeField] private GameObject _face,_hair;
    [SerializeField] private float turnSpeed = 10.0f;   // 回転速度
    [SerializeField] private Transform player;          // 注視対象プレイヤー

    [SerializeField] private float distance = 15.0f;    // 注視対象プレイヤーからカメラを離す距離
    [SerializeField] private Quaternion vRotation;      // カメラの垂直回転(見下ろし回転)
    [SerializeField] public  Quaternion hRotation;      // カメラの水平回転

    void Start ()
    {
        // 回転の初期化
        vRotation = Quaternion.Euler(0, 0, 0);         // 垂直回転(X軸を軸とする回転)は、30度見下ろす回転
        hRotation = Quaternion.identity;                // 水平回転(Y軸を軸とする回転)は、無回転
        transform.rotation = hRotation * vRotation;     // 最終的なカメラの回転は、垂直回転してから水平回転する合成回転

        // 位置の初期化
        // player位置から距離distanceだけ手前に引いた位置を設定します
        transform.position = player.position - transform.rotation * Vector3.forward * distance; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {CameraChange();}
    }

    void LateUpdate()
    {
        // 水平回転の更新
        if(Input.GetMouseButton(0))
        {
            hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
            vRotation *= Quaternion.Euler(Input.GetAxis("Mouse Y") * turnSpeed, 0, 0);
        }
        Debug.Log("vRotation.x : " + vRotation.x);
        // カメラの回転(transform.rotation)の更新
        // 方法1 : 垂直回転してから水平回転する合成回転とします
        transform.rotation = hRotation * vRotation;

        // カメラの位置(transform.position)の更新
        // player位置から距離distanceだけ手前に引いた位置を設定します(位置補正版)
        transform.position = player.position + new Vector3(0, 1, 0) - transform.rotation * Vector3.forward * distance; 
    }

    void CameraChange()
    {
        //視点切り替え
        distance = (distance == 0.0f) ? 30.0f : 0.0f;
        _face.SetActive(!_face.activeSelf);
        _hair.SetActive(!_hair.activeSelf);
    }
}