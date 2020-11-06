using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {

    //プレイヤーを格納する変数
    public GameObject player;
    [SerializeField] Vector3 cameraVec;  //プレイヤーとの距離を調整する変数
    [SerializeField] Vector3 cameraDir;  //プレイヤーとの角度を調整する変数direction
    private Vector3 offset;
    private Vector3 rotset;
    private float LRtri;    //LBトリガーとRBトリガーを取る
    private Vector3 PlayerAngle;    //プレイヤーのワールド座標アングル
    private Vector3 CameraAngele;   //カメラ(このオブジェクト)のワールド座標アングル
    private Vector3 DirectionAngle; //カメラがプレイヤーの正面を向くアングル
	// Use this for initialization
	void Start () {
        //offset = transform.position - player.transform.position;
        //transform.position
        offset = new Vector3((transform.position.x + cameraVec.x) - player.transform.position.x, (transform.position.y + cameraVec.y) - player.transform.position.y,
            (transform.position.z + cameraVec.z) - player.transform.position.z);

        rotset = new Vector3((transform.rotation.x + cameraDir.x) - player.transform.position.x, (transform.rotation.y + cameraDir.y) - player.transform.position.y,
            (transform.rotation.z + cameraDir.z) - player.transform.position.z);
        transform.localRotation = Quaternion.Euler(cameraDir.x, cameraDir.y, cameraDir.z);//


        //CameraAngele = transform.eulerAngles;
        //PlayerAngle = player.transform.eulerAngles;
        DirectionAngle = new Vector3(0,0,0);    //念のため初期化しておく()
	}

	// Update is called once per frame
	void Update () {
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
              transform.RotateAround(player.transform.position, Vector3.up, -5.0f);
              // transform.RotateAround(Vector3.zero,Vector3.up,-2.0f);
              offset = transform.position - player.transform.position;
              //rotaflg = true;//↓のコメント文になってるやつで使ってた
              Debug.Log("カメラボタン瞬間の判定");
        }

        if (LRtri < 0)//右
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(player.transform.position, Vector3.up, 5.0f);
            // transform.RotateAround(Vector3.zero,Vector3.up,-2.0f);
            offset = transform.position - player.transform.position;
        }

        if (Input.GetButtonDown("LB"))//コントローラー操作//長押しGetButton//押した時GetButtonDown//離れた時GetButtonUp
          {
              //Debug.Log(PlayerAngle);
              //Debug.Log("LBボタンが押された");
              DirectionAngle = (CameraAngele - PlayerAngle);    //カメラのアングルとプレイヤーのアングルを引いて必要な回転量を求めるよ

              //RotateAround(円運動の中心,進行方向,速度)
              transform.RotateAround(player.transform.position, Vector3.up, DirectionAngle.y * -1); //さっき計算したDirectionAngle.yに-1をかけるとプレイヤーの正面を向くよ
              offset = transform.position - player.transform.position;
          }
	}
}
