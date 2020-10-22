using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    //private Vector3 ido;

    public float Rspeed; //剣の傾くスピード

    public int Angleflg;    //剣が傾いているかどうか?

    public GameObject CameraObject;//カメラを入れる

    private float Rx; //右スティックのx方向のImputの値
    private float Rz; //右スティックのz方向のInputの値
    private Rigidbody rigd;

    // Use this for initialization
    void Start()
    {
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
       //CameraObject.transform.eulerAngles = CameraTransform;
//CameraObject.transform.eulerAngles;
        //transform.eulerAngles
        Angleflg = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Rx = Input.GetAxis("Horizontal2"); //右スティックのx方向のInputの値を取得
        Rz = Input.GetAxis("Vertical2"); //右スティックのz方向のInputの値を取得
        float A = 0;

        float radian = Mathf.Atan2(Rz, Rx) * Mathf.Rad2Deg;//

        if (radian < 0)
        {
            radian += 360;
        }


        Debug.Log(A);

        if (Rz > 0 && Rx == 0)//前
        {
            Debug.Log("前");
            // transform.localRotation = Quaternion.Euler(90,0, 0);
            //CameraObject.transform.eulerAngles.yを使ってカメラの向いている方向を前方向としている。以下↓のソースで使用している目的も同様です。
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }
        if (Rz > 0 && Rx > 0)//右前
        {
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }

        if (Rz < 0 && Rx == 0)//後ろ
        {
            Debug.Log("後ろ");
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }

        if (Rz < 0 && Rx > 0)//右後ろ
        {
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }

        if (Rx > 0 && Rz == 0)//右
        {
            Debug.Log("右");
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }


        if (Rx < 0 && Rz == 0)//左
        {
            Debug.Log("左");
            transform.localRotation = Quaternion.Euler(90,0, 0);//に傾き
            Angleflg = 1;
        }

        if (Rx < 0 && Rz > 0)//左前
        {
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }

        if (Rx < 0 && Rz < 0)//左後ろ
        {
            transform.localRotation = Quaternion.Euler(90,0, 0);//
            Angleflg = 1;
        }

        else if (Rx == 0 && Rz == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);//初期地点に傾き
            Angleflg = 0;
        }

        if (Angleflg == 1)
        {
            transform.localRotation = Quaternion.Euler(90,0, 0);//
        }
    }
}
