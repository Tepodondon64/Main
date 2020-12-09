using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {

    public GameObject player;   //プレイヤーを格納する変数
    private Transform Dummyplayer;  //ダミープレイヤーを格納する変数

    [SerializeField]
    Vector3 cameraVec;  //プレイヤーとの距離を調整する変数
    [SerializeField]
    Vector3 cameraDir;  //プレイヤーとの角度を調整する変数

    private Vector3 offset; //距離を入れる
    private Vector3 rotset; //角度を入れる
    private Vector3 PlayerAngle;    //プレイヤーのワールド座標アングル
    private Vector3 DummyPlayerAngle;    //ダミープレイヤーのワールド座標アングル
    private Vector3 CameraAngele;   //カメラ(このオブジェクト)のワールド座標アングル
    private Vector3 DirectionAngle; //カメラがプレイヤーの正面を向くアングル


    private float LRtri;    //LBトリガーとRBトリガーを取る
    private float RotaSpeed = 2.0f;    //カメラ回転スピード
    private float RotaFSpeed = 0.1f;    //正面に向くカメラの回転スピード

    private bool Frontflg;  //カメラはプレイヤーの正面方向に向きたいですか？     false:いいえ   true:はい、向きたいです。

    private int Count = 50; //どれくらい時間正面方向を向き続ける処理を行うか？

    void Start()
    {

        offset = new Vector3((cameraVec.x) - player.transform.position.x, (cameraVec.y) - player.transform.position.y,
           (cameraVec.z) - player.transform.position.z);


        transform.localRotation = Quaternion.Euler(cameraDir.x, cameraDir.y, cameraDir.z);

        DirectionAngle = new Vector3(0, 0, 0);    //念のため初期化しておく()
        Frontflg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ここでワールド座標を取る
        CameraAngele = transform.eulerAngles;
        PlayerAngle = player.transform.eulerAngles;

        //Trigger
        LRtri = Input.GetAxis("L_R_Trigger");


        // まずはカメラ位置をプレイヤーに追従させて...
        transform.position = player.transform.position + offset;

        // プレイヤーを中心にカメラを回すと、プレイヤーとカメラの相対位置が
        // 変化するはずなので、RotateAroundの後でoffsetを更新する

        if (LRtri > 0)//左
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(player.transform.position, Vector3.up, (-1 * RotaSpeed));
            offset = transform.position - player.transform.position;
            //Debug.Log("左カメラボタンが押されてます！");
        }

        if (LRtri < 0)//右
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(player.transform.position, Vector3.up, RotaSpeed);
            offset = transform.position - player.transform.position;
        }

        if (Input.GetButtonDown("LB"))//コントローラー操作//長押しGetButton//押した時GetButtonDown//離れた時GetButtonUp
        {
            Frontflg = true;
            Dummyplayer = player.transform;
            DummyPlayerAngle = player.transform.eulerAngles;
        }


        if (Frontflg == true)
        {
            DirectionAngle = (CameraAngele - DummyPlayerAngle);    //カメラのアングルとプレイヤーのアングルを引いて必要な回転量を求めるよ

            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(Dummyplayer.transform.position, Vector3.up, DirectionAngle.y * (-1 * RotaFSpeed)); //さっき計算したDirectionAngle.yに-1をかけるとプレイヤーの正面を向くよ
            offset = transform.position - Dummyplayer.transform.position;
            Count--;
            if (Count < 0)
            {
                Frontflg = false;
                Count = 50;
            }
        }
    }
}
