using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private GameObject HAL2;
    // Start is called before the first frame update
    void Start()
    {
        HAL2 = GameObject.Find("newHAL2");
    }

    // Update is called once per frame
    void Update()
    {
        //左シフトが押されている時
        if(Input.GetKey(KeyCode.LeftShift)){
            //ハルを中心に-5f度回転
            transform.RotateAround(HAL2.transform.position, Vector3.up, -5f);
        }
        //右シフトが押されている時
        else if(Input.GetKey(KeyCode.RightShift)){
            //ハルを中心に5f度回転
            transform.RotateAround(HAL2.transform.position, Vector3.up, 5f);
        }
    }
}
