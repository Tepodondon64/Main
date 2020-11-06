using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {

    //private Vector3 ido;

    public float Mspeed; //プレイヤーの動くスピード
    public GameObject CameraObject;//カメラを入れる
    public int JumpPower = 1;
    public int JumpSpeed = 20;


    private Vector3 Player_pos; //プレイヤーのポジション
    private float Lx; //左スティックのx方向のImputの値
    private float Lz; //左スティックのz方向のInputの値
    private float Rx; //右スティックのx方向のImputの値
    private float Rz; //右スティックのz方向のInputの値
    private Rigidbody rigd;
    private int Angleflg;

    private int A;
	// Use this for initialization
	void Start () {
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得

        Angleflg = 0;
	}
	
	// Update is called once per frame
	void Update () {

        Lx = Input.GetAxis("Horizontal"); //左スティックのx方向のInputの値を取得
        Lz = Input.GetAxis("Vertical") ; //左スティックのz方向のInputの値を取得

        Rx = Input.GetAxis("Horizontal2"); //右スティックのx方向のInputの値を取得
        Rz = Input.GetAxis("Vertical2"); //右スティックのz方向のInputの値を取得

        float A = 0;

        float Lradian = Mathf.Atan2(Lz, Lx) * Mathf.Rad2Deg;//左スティックの倒した時の正確な角度

        if (Lradian < 0)
        {
            Lradian += 360;
        }


        float Rradian = Mathf.Atan2(Rz, Rx) * Mathf.Rad2Deg;//右スティックの倒した時の正確な角度

        if (Rradian < 0)
        {
            Rradian += 360;
        }

        //rigd.AddForce(Lx, 0, Lz);//移動処理
       // rigd.AddForce(Lx, 0, Lz);//移動処理


        if (Lx != 0 || Lz != 0)//左スティックの回転てきなやつ(動かしてたら通る)
        {
            


            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + Lradian * -1 + 90, 0);//プレイヤーの回転

            //rigd.MovePosition(transform.localPosition + (transform.rotation * new Vector3(transform.forward.x * Mspeed,
             //   0, transform.forward.z * Mspeed)));

            rigd.velocity += (new Vector3(transform.forward.x * Mspeed, 0, transform.forward.z * Mspeed));//プレイヤーの移動
            //rigd.velocity = new Vector3(0, 0, 1 * Mspeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
        }

        else if (Lx == 0 && Lz == 0)//左スティックの回転てきなやつ(動かして無ければ通る
        {
            rigd.velocity += (new Vector3(0, 0, 0));    //プレイヤーの移動していないとき

            //transform.up * JumpPower
           // rigd.velocity = (new Vector3(0, transform.up.y * JumpPower, 0));
            
           // rigd.velocity = new Vector3(Lx * Mspeed, rigd.mass * -1, Lz * Mspeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
        }

        //(CameraObject.transform.eulerAngles.y + Lradian)
        //Debug.Log(radian);//
        //Debug.Log(A);
        //(移動してないときも)常に重力をかけている //RigidbodyのMass(重さ)
        //rigd.velocity = new Vector3(  Lx * Mspeed, rigd.mass * -1, Lz * Mspeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
        if (Lz != 0)
        {
            //rigd.velocity = transform.forward * 1 * Mspeed;
        }
        else
        {
            //rigd.velocity = transform.forward * 0;
        }

        
        //rigd.MovePosition(transform.forward * 1 * Mspeed);

        //Vector3 diff = transform.position - Player_pos; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得

       // rigd.rotation
        //Player_pos = transform.position; //プレイヤーの位置を更新

        if (Rz > 0 && Rx == 0)//前
        {
            Debug.Log("前");
            // transform.localRotation = Quaternion.Euler(90,0, 0);
            //CameraObject.transform.eulerAngles.yを使ってカメラの向いている方向を前方向としている。以下↓のソースで使用している目的も同様です。
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y, 0);//
            Angleflg = 1;
        }
        if (Rz > 0 && Rx > 0)//右前
        {
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + 45, 0);//
            Angleflg = 1;
        }

        if (Rz < 0 && Rx == 0)//後ろ
        {
            Debug.Log("後ろ");
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + 180, 0);//
            Angleflg = 1;
        }

        if (Rz < 0 && Rx > 0)//右後ろ
        {
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + 135, 0);//
            Angleflg = 1;
        }

        if (Rx > 0 && Rz == 0)//右
        {
            Debug.Log("右");
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + 90, 0);//
            Angleflg = 1;
        }


        if (Rx < 0 && Rz == 0)//左
        {
            Debug.Log("左");
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y - 90, 0);//に傾き
            Angleflg = 1;
        }

        if (Rx < 0 && Rz > 0)//左前
        {
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y - 45, 0);//
            Angleflg = 1;
        }

        if (Rx < 0 && Rz < 0)//左後ろ
        {
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y - 135, 0);//
            Angleflg = 1;
        }

        else if (Rx == 0 && Rz == 0)
        {
          //  transform.localRotation = Quaternion.Euler(0, 0, 0);//初期地点に傾き
            Angleflg = 0;
        }

        if (Angleflg == 1)
        {
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + Rradian * -1 + 90, 0);//
        }
        //ジャンプ
        // ★追加（ジャンプ）//Input.GetButtonDown("RT")
        if (Input.GetButtonDown("RB"))//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            //rigd.mass * -1
            Debug.Log("ジャンプだ!");
            //rigd.velocity = Vector3.up * JumpSpeed;
            //rigd.velocity = new Vector3(0, (JumpPower) + (rigd.mass) * JumpSpeed, 0);
            rigd.velocity = transform.up * (JumpPower + rigd.mass -1);
        }
	}
}
