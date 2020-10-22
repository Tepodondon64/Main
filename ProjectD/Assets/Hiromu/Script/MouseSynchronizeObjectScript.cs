using UnityEngine;
using System.Collections;
using UnityEngine.UI;//デバッグ用文字を表示させるために必要
//using System.Collections.Generic;
public class MouseSynchronizeObjectScript : MonoBehaviour
{
    // 位置座標
    private Vector3 position;

    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    private bool ClickFlg;  //左クリックが押されているか？
    private bool MoveFlg;  //初回の右クリックが押されているか？
    
    private Rigidbody rb;
    public float Z;
    public float Y;
    public GameObject PositionObject = null;
    private Text PositionText;


    // Use this for initialization
    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);//真ん中基準
        // オブジェクトからTextコンポーネントを取得
        PositionText = PositionObject.GetComponent<Text>();

        ClickFlg = false;
        MoveFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))//右クリック
        {
            MoveFlg = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);//傾きを戻す
        }
        if (Input.GetMouseButtonDown(1))//右クリック
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // 同上
        }
        //if (position.x > 800)
        //{
        //    position.x = 180;
        //}

        //if(transform.localRotation.z > 90)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, 90);//傾きを戻す
        //}

        //if (transform.localRotation.z > -90)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, -90);//傾きを戻す
        //}

        if(MoveFlg == true && ClickFlg == false)
        {
        

        Text();

        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = Z;//10がおすすめ
        //position.y = position.y-Y;//
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position * -1);
        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;
        ///////

        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);//真ん中基準

        //if (position.x > 800)
        //{
        //    position.x = 800;
        //}
        //if (position.x < 200)
        //{
        //    position.x = 200;
        //}

        //if (position.y > 700)
        //{
        //    position.y = 700;
        //}
        //if (position.y < 0)
        //{
        //    position.y = 0;
        //}
        //if (position.x > centralpoint.PointPosition.position.x)
        //{

        //}
           // position.x = 180;
        transform.localRotation = Quaternion.Euler((position.y + 110)*-1, position.x + 110, 0);//

        //Quaternion.Inverse


        //transform.localRotation = Quaternion.Euler(position.y*-1, 0, position.x);//


        ///////
        //if (position.x > Camera.main.transform.position.x)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, -90);//
        //}
        //if (Camera.main.transform.position.x < position.x)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, +90);//
        //}

        }

        if (Input.GetMouseButtonDown(0) && ClickFlg == false)
        {
            print("いま左ボタンが押された");
            //transform.Rotate(new Vector3(0, 0, 90));
            transform.localRotation = Quaternion.Euler(0, 0, 90);//左向きに傾き
            ClickFlg = true;
        }

        if (Input.GetMouseButton(0) && ClickFlg == true)
        {
            print("左ボタンが押されている");
            //transform.Rotate(new Vector3(0, 0, 5));
            //// 回転しない設定と動かない設定
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            print("いま左ボタンが離された");
            //transform.Rotate(new Vector3(0, 0, -90));
            //// 回転しない設定と動かない設定を解除
            rb.constraints = RigidbodyConstraints.None;
            transform.localRotation = Quaternion.Euler(0, 0, 0);//傾きを戻す
            ClickFlg = false;
        }
    }
    void Text()
    {

        //PositionText.text = "X:" + position.x.ToString() + "Y:" + position.y.ToString() + "Z:" + position.y.ToString();
        //マウス座標の表示
        PositionText.text = "X:" + position.x.ToString() + "  Y:" + position.y.ToString();
       // print("X:" + position.x.ToString() + " Y:" + position.y.ToString() + " Z:" + position.y.ToString());
    }
}