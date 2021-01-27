using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {

    public GameObject CameraObject;//カメラを入れる
    public float Mspeed; //プレイヤーの動くスピード
    public int JumpPower = 1;


    private Vector3 Player_pos; //プレイヤーのポジション

    private float Lx; //左スティックのx方向のImputの値
    private float Lz; //左スティックのz方向のInputの値
    private float Rx; //右スティックのx方向のImputの値
    private float Rz; //右スティックのz方向のInputの値

    private Rigidbody rigd;

    private int Angleflg;
    private int GrandCount;

    private bool Grandflg;   //地面にいるか？ //false:地面にいない    true:地面にいる

    private float posY;

    Animator animator;

    private bool Happyflg;
    private bool IdleLong_flg;
    private bool Know_Down_flg;
    private bool Know_Back_flg;
    //private float Min = 3 , Max = 5;

    void Start () {
        Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        rigd = GetComponent<Rigidbody>(); //プレイヤーのRigidbodyを取得
        Angleflg = 0;
        Grandflg = true;//
        GrandCount = 5;
        animator = GetComponent<Animator>();
        Happyflg = false;
        IdleLong_flg = false;
        Know_Down_flg = false;
        Know_Back_flg = false;

    }

    void OnCollisionStay(Collision other)//触れているとき
    {
        if (other.gameObject.tag == "Untagged")//タグ無しのやつ    ※タグが増えてきたら変更の余地あり
        {
            GrandCount--;
            Grandflg = true;
            animator.SetBool("Jump_Bool", false);//ジャンプアニメーションを止める
        }
    }
    void OnCollisionExit(Collision other)//触れていないとき
    {
        //Debug.Log("こん");
        Grandflg = false;
        GrandCount = 5;
    }


	// Update is called once per frame
	void Update () {
        Lx = Input.GetAxis("Horizontal"); //左スティックのx方向のInputの値を取得
        Lz = Input.GetAxis("Vertical") ; //左スティックのz方向のInputの値を取得

        Rx = Input.GetAxis("Horizontal2"); //右スティックのx方向のInputの値を取得
        Rz = Input.GetAxis("Vertical2"); //右スティックのz方向のInputの値を取得

        posY = transform.position.y;

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


        if (Lx != 0 || Lz != 0)//左スティックの回転てきなやつ(動かしてたら通る)
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            if (Rradian == 0 && Angleflg == 0)//攻撃中では無いとき
            {

                transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + Lradian * -1 + 90, 0);//プレイヤーの回転

                if(Grandflg == true)//地面にいるとき
                {
                    animator.SetBool("Walk_Bool", true);//歩くアニメーションをする
                    if (GrandCount < 0)
                    {
                        rigd.velocity += (new Vector3(transform.forward.x * Mspeed, 0, transform.forward.z * Mspeed));//プレイヤーの移動
                    }

                    if (GrandCount > 0)//着地して少しの間はプレイヤーの移動速度を上げます
                    {
                        //Debug.Log("着地加速終了までのこり<color=red><size=20>" + GrandCount + "</size></color>秒");
                        //Debug.Log("<color=red><size=30>おまえ</size></color>" + "<size=20>はもう</size>" + "<color=red><size=30>死</size></color>" + "<size=20>んでいる....</size>");
                        rigd.velocity += (new Vector3(transform.forward.x * Mspeed*2, 0, transform.forward.z * Mspeed*2));//プレイヤーの移動
                    }

                }
                if (Grandflg == false)//空中にいるとき
                {
                    rigd.velocity += (new Vector3(transform.forward.x * Mspeed*2, -1, transform.forward.z * Mspeed*2));//プレイヤーの移動
                }
            }
            else if (Rradian != 0 || Angleflg == 1 && Grandflg == false)//攻撃中で空中にいるとき
            {
                animator.SetBool("Walk_Bool", false);//歩くアニメーションを止める
                rigd.velocity += (new Vector3(transform.forward.x * Mspeed * 2, -1, transform.forward.z * Mspeed * 2));//プレイヤーの移動
            }
        }

        else if (Lx == 0 && Lz == 0)//左スティックの回転てきなやつ(動かして無ければ通る
        {
            if (Grandflg == true)//地面にいるとき
            {

                rigd.velocity += (new Vector3(0, 0, 0));    //プレイヤーの移動していないとき
                animator.SetBool("Walk_Bool", false);//歩くアニメーションを止める
                animator.SetBool("Idle_Bool", true);//待機アニメーションをする
            }
            if (Grandflg == false)//空中にいるとき
            {
                rigd.velocity += (new Vector3(0, -1, 0));    //プレイヤーの移動していないとき
            }
        }


        /***************************
         * 右スティックの操作判定***
         ***************************/

        if (Rz > 0 && Rx == 0)//前
        {
            //Debug.Log("前");
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
            //Debug.Log("後ろ");
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
            //Debug.Log("右");
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + 90, 0);//
            Angleflg = 1;
        }


        if (Rx < 0 && Rz == 0)//左
        {
            //Debug.Log("左");
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
            animator.SetBool("Atack_Bool", false);//攻撃アニメーションを止める
        }

        if (Angleflg == 1)  //攻撃中
        {
            animator.SetBool("Atack_Bool", true);//攻撃アニメーションをする
            transform.localRotation = Quaternion.Euler(0, CameraObject.transform.eulerAngles.y + Rradian * -1 + 90, 0);//
        }


        // ★（ジャンプ）//
        if (Input.GetButtonDown("RB"))//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            animator.SetBool("Walk_Bool", false);//歩くアニメーションを止める
            //animator.SetBool("Atack_Bool", false);//攻撃アニメーションを止める
            animator.SetBool("Jump_Bool", true);//ジャンプアニメーションをする
            rigd.velocity = transform.up * (JumpPower + rigd.mass -1);
            //※今の所は∞ジャンプ可能修正予定あり。
        }

        if (Input.GetButtonDown("Fire1") && Happyflg == false)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            animator.SetBool("Happy_Bool", true);//Happyアニメーションを始める
            Happyflg = true;
        }

       else if (Input.GetButtonDown("Fire1") && Happyflg == true)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", true);//待機アニメーションを始める
            animator.SetBool("Happy_Bool", false);//Happyアニメーションを止める
            Happyflg = false;
        }

        if (Input.GetButtonDown("Fire2") && IdleLong_flg == false)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            animator.SetBool("IdleLong_Bool", true);//長い待機アニメーションを始める
            IdleLong_flg = true;
        }

       else if (Input.GetButtonDown("Fire2") && IdleLong_flg == true)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", true);//待機アニメーションを始める
            animator.SetBool("IdleLong_Bool", false);//長いアニメーションを止める
            IdleLong_flg = false;
        }

        if (Input.GetButtonDown("Fire3") && Know_Back_flg == false)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            animator.SetBool("Know_Back_Bool", true);//ノックバックアニメーションを始める
            Know_Back_flg = true;
        }

        else if (Input.GetButtonDown("Fire3") && Know_Back_flg == true)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", true);//待機アニメーションを始める
            animator.SetBool("Know_Back_Bool", false);//ノックバックアニメーションを止める
            Know_Back_flg = false;
        }

        if (Input.GetButtonDown("Jump") && Know_Down_flg == false)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", false);//待機アニメーションを止める
            animator.SetBool("Know_Down_Bool", true);//ノックダウンアニメーションを始める
            Know_Down_flg = true;
        }

        else if (Input.GetButtonDown("Jump") && Know_Down_flg == true)//コントローラー操作///GetButton//GetButtonDown//GetButtonUp
        {
            animator.SetBool("Idle_Bool", true);//待機アニメーションを始める
            animator.SetBool("Know_Down_Bool", false);//ノックダウンアニメーションを止める
            Know_Down_flg = false;
        }

        //Know_Down_flg
    }
}
