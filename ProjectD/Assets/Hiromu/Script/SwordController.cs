using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    //private Vector3 ido;

    public float Rspeed; //剣の傾くスピード

    //public GameObject CameraObject;//カメラを入れる

    public bool AttackFlg;  //攻撃が入力されたらtrueになる

    private float Rx; //右スティックのx方向のImputの値
    private float Rz; //右スティックのz方向のInputの値
    private Rigidbody rigd;

    private Vector3 Rota;

    // Use this for initialization
    void Start()
    {
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
       //CameraObject.transform.eulerAngles = CameraTransform;
//CameraObject.transform.eulerAngles;
        //transform.eulerAngles

        Rota = new Vector3(0, 0, 0);    //念のため初期化しておく()

        AttackFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = Rspeed * Time.deltaTime;   //棒を振るスピードの調整

        Rx = Input.GetAxis("Horizontal2"); //右スティックのx方向のInputの値を取得
        Rz = Input.GetAxis("Vertical2"); //右スティックのz方向のInputの値を取得

        float radian = Mathf.Atan2(Rz, Rx) * Mathf.Rad2Deg;//

        if (radian < 0)
        {
            radian += 360;
        }


       // Debug.Log(A);

        if (Rz > 0 && Rx == 0)//前
        {
            Debug.Log("前");
            //transform.localRotation = Quaternion.Euler(90,0, 0);//
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }
        if (Rz > 0 && Rx > 0)//右前
        {
           // transform.localRotation = Quaternion.Euler(90,0, 0);//
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        if (Rz < 0 && Rx == 0)//後ろ
        {
            Debug.Log("後ろ");
            //transform.localRotation = Quaternion.Euler(90,0, 0);//
           // transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        if (Rz < 0 && Rx > 0)//右後ろ
        {
            //transform.localRotation = Quaternion.Euler(90,0, 0);//
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        if (Rx > 0 && Rz == 0)//右
        {
            Debug.Log("右");
           // transform.localRotation = Quaternion.Euler(90,0, 0);//
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }


        if (Rx < 0 && Rz == 0)//左
        {
            Debug.Log("左");
            //transform.localRotation = Quaternion.Euler(90,0, 0);//に傾き
           // transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
          // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        if (Rx < 0 && Rz > 0)//左前
        {
            //transform.localRotation = Quaternion.Euler(90,0, 0);//
            //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        if (Rx < 0 && Rz < 0)//左後ろ
        {
            //transform.localRotation = Quaternion.Euler(90,0, 0);//
           // transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
           // transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f, 0, 0), step);
            AttackFlg = true;
        }

        else if (Rx == 0 && Rz == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);//初期地点に傾き
            AttackFlg = false;
            Rota = new Vector3(0, 0, 0);    //初期化
        }

        if (AttackFlg == true)
        {
            Rota.x++;
            //transform.localRotation = Quaternion.Euler(90, 0, 0);//
            if (Rota.x * Rspeed < 90)
            {
                transform.localRotation = Quaternion.Euler(Rota.x * Rspeed, 0, 0);//
            }
            if (Rota.x * Rspeed >= 90)
            {
                //transform.localRotation = Quaternion.Euler(90, 0, 0);//
            }
        }

        //if (AttackFlg == true)
        //{
        //    transform.localRotation = Quaternion.Euler(90,0, 0);//
        //}
    }
}
