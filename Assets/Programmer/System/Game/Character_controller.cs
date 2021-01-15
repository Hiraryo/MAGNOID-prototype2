using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    public GameObject HAL2;

    //回転の補完速度
    [SerializeField]
    private float rotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //左手方向のキーを押すとZ軸を軸にして時計回りに回転する
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            HAL2.transform.Rotate(Vector3.up * rotationSpeed,Space.Self);
        }
        //右手方向のキーを押すとZ軸を軸にして時計回りに回転する
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            HAL2.transform.Rotate(Vector3.down * rotationSpeed,Space.Self);
        }
    }
}
