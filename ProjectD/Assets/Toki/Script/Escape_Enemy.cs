using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_Enemy : MonoBehaviour
{

    private Rigidbody rb;
    private CapsuleCollider cc;

    private ParticleSystem[] PS;

    /*
    private Rigidbody[] ragdollrb;
    private CapsuleCollider[] ragdollcap;


    private float ragdoll_time = 1.0f;
    private float ragdoll_count = 0.0f;
    */

    [HideInInspector] public GameObject Player = null;

    private SwordController P_Sword;

    [SerializeField] private GameObject[] points;

    private int now_point = 0;

    [SerializeField] private float speed = 0;

    //private ParticleSystem PS;

    public float enemy_state = 1;   //0:存在しない, 1:アイドル, 2:歩行, 3:横歩行, 4:攻撃, 5:起き上がり,6:死

    [SerializeField] private float range_level = 10.0f; //プレイヤーの発見段階レベル２(横方向へ切り返したりする)

    private float change_direction_time = 1.0f;
    private float change_direction_count = 0.0f;

    private float reverse_direction_time = 0.5f;
    private float reverse_direction_count = 0.5f;

    
    private bool check_avoid = false;//回避判定が一度終わったか?フラグ
    private bool avoid_flg = false;  //敵の回避フラグ


    private float panic_time = 1.5f;
    private float panic_count = 1.5f;

    private float death_time = 1.0f;
    private float death_count = 0.0f;

    private int not_find_count = 0;


    private Animator anim;

    private AnimatorClipInfo clipInfo;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        PS = GetComponentsInChildren<ParticleSystem>();

        foreach(ParticleSystem particle in PS)
        {
            particle.Stop();
        }

        P_Sword = GameObject.FindWithTag("Sword").GetComponent<SwordController>();

        /*
        ragdollrb = GetComponentsInChildren<Rigidbody>();
        ragdollcap = GetComponentsInChildren<CapsuleCollider>();
        */

        //ragdollbox = GetComponentInChildren<BoxCollider>();

        /*
        foreach (Rigidbody rigidbody in ragdollrb)
        {
            rigidbody.isKinematic = true;
            anim.enabled = true;
        }
        foreach(CapsuleCollider capsule in ragdollcap)
        {
            capsule.isTrigger = true;
        }

        cc.isTrigger = false;
        rb.isKinematic = false;
        */

        //PS.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        clipInfo = anim.GetCurrentAnimatorClipInfo(0)[0];

        Debug.Log(clipInfo.clip.name);

        if (enemy_state != 6)
        {
            E_Move();
        }

        if(enemy_state == 6)
        {
            E_Death();
        }

        SetAnimState();
    }


    void E_Move()
    {
        if (Player != null)
        {

            Vector3 middle = transform.position - Player.transform.position;

            float angle = Mathf.Atan2(middle.x, middle.z) * Mathf.Rad2Deg;

            float distance = Vector3.Distance(transform.position, Player.transform.position);


            //Debug.Log(distance);

            if (distance < range_level)
            {

                if(P_Sword.AttackFlg)
                {
                    if (!check_avoid)
                    {

                        switch (Random.Range(0, 2 + 1))
                        {
                            case 0:

                                avoid_flg = false;
                                break;

                            case 1:

                               

                            case 2:
                                transform.position += transform.right * 2;
                                avoid_flg = true;
                                break;
                        }

                        check_avoid = true;
                    }
                }
                else
                {
                    check_avoid = false;
                    avoid_flg = false;
                }


                //切り返し時以外は、プレイヤーの反対に進む
                if(change_direction_count >= 0)
                {
                    reverse_direction_count += Time.deltaTime;

                    if(reverse_direction_count > reverse_direction_time)
                    {
                        
                        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                        reverse_direction_count = 0f;
                    }
                    
                    change_direction_count += Time.deltaTime;
                }
                //切り返し後、数秒はその方向にのみ進む
                else
                {
                    change_direction_count += Time.deltaTime;
                }
                


                if (change_direction_count > change_direction_time)
                {
                    switch(Random.Range(0, 3 + 1))
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y + 45f, 0));
                            change_direction_count = -2.0f;
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y + 90f, 0));
                            change_direction_count = -2.0f;
                            break;

                        case 2:
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y - 45f, 0));
                            change_direction_count = -2.0f;

                            break;

                        case 3:
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y - 90f, 0));
                            change_direction_count = -2.0f;
                            break;
                    }

                    
                }

            }
            else if(distance < (range_level * 2))
            {

                reverse_direction_count += Time.deltaTime;

                if(reverse_direction_count > reverse_direction_time)
                {
                    reverse_direction_count = 0;
                    
                    transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                }

                

            }
            else
            {
                panic_count += Time.deltaTime;

                if (panic_count > panic_time)
                {
                    
                    transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                    panic_count = 0;
                    not_find_count++;
                }
                
                if(not_find_count > 3)
                {
                    Player = null;
                    not_find_count = 0;
                    panic_count = panic_time;
                }
            }
            


            enemy_state = 2;
            rb.MovePosition(transform.position + transform.forward * speed);
        }

        else
        {


            if(points.Length != 0)
            {

                enemy_state = 2;

                Vector3 middle = points[now_point].transform.position - transform.position;
                float angle = Mathf.Atan2(middle.x, middle.z) * Mathf.Rad2Deg;
                float distance = Vector3.Distance(transform.position, points[now_point].transform.position);

                if(distance > 2)
                {
                    
                     transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                }
                else
                {

                    if((points.Length - 1) >= (now_point + 1))
                    {
                        now_point++;
                    }
                    else
                    {
                        now_point = 0;
                    }
                   
                }

                rb.MovePosition(transform.position + transform.forward * (speed / 2));

            }
            else
            {
                enemy_state = 1;
            }
           
        }

    }

    void E_Death()
    {

        if(clipInfo.clip.name == "アーマチュア|Down")
        {
            death_count += Time.deltaTime;

            if(death_count > death_time)
            {
                Destroy(gameObject);
            }
            
        }

        
    }


    private void SetAnimState()
    {
        anim.SetFloat("State", enemy_state);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (P_Sword.AttackFlg)
        {


            if (!avoid_flg)
            {


                if (collision.gameObject.tag == "Sword")
                {
                    enemy_state = 6;

                    foreach (ParticleSystem particle in PS)
                    {
                        particle.Play();
                    }
                }
            }
        }
    }





    private void JOJO_RotateY(float y, float rotate2flame)
    {
        // 目的の角度を0 ～ 360に変換
        while(y >= 360)
        {
            y -= 360;
        }
        while(y < 0)
        {
            y += 360;
        }

        // 現在の角度を0 ～ 360に変換
        while (transform.rotation.y >= 360)
        {
            transform.Rotate(0, -360, 0);
        }
        while (transform.position.y < 0)
        {
            transform.Rotate(0, 360, 0);
        }





        if(transform.rotation.y != y)
        {
            float reverse_angle = transform.rotation.y - 180;

            if(reverse_angle < 0)
            {
                reverse_angle += 360f;
            }

            //上にある
            if(transform.rotation.y < 180)
            {
                if(transform.rotation.y < y && reverse_angle >= y)
                {
                    if((transform.rotation.y + rotate2flame) >= y)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                    }
                    else
                    {
                        transform.Rotate(0, rotate2flame, 0);
                    }
                }
                else if((transform.rotation.y > y && 0 <= y) || (reverse_angle < y && 360 > y))
                {
                    if(transform.rotation.y - rotate2flame < 0)
                    {
                        float range = transform.rotation.y - rotate2flame;

                        if(range < 0)
                        {
                            range += 360;
                        }

                        if((transform.rotation.y > y && 0 <= y) || (range <= y && 360 > y))
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.Rotate(0, -rotate2flame, 0);
                        }
                    }
                    else
                    {
                        if ((transform.rotation.y - rotate2flame) <= y)
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.Rotate(0, -rotate2flame, 0);
                        }
                    }
                    
                }
                

            }
            //下にある
            else
            {
                if(transform.rotation.y > y && reverse_angle <= y)
                {
                    if((transform.rotation.y - rotate2flame) <= y)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                    }
                    else
                    {
                        transform.Rotate(0, -rotate2flame, 0);
                    }
                }
                else if((transform.rotation.y < y && 360 > y) || (reverse_angle > y && 0 <= y))
                {
                    if(transform.rotation.y + rotate2flame >= 360)
                    {
                        float range = transform.rotation.y + rotate2flame;

                        if(range >= 360)
                        {
                            range -= 360;
                        }

                        if((transform.rotation.y < y && 360 > y) || (range >= y && 0 <= y))
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.Rotate(0, rotate2flame, 0);
                        }
                    }
                    else
                    {
                        if ((transform.rotation.y + rotate2flame) >= y)
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.Rotate(0, rotate2flame, 0);
                        }
                    }
                    
                }

            }
            

        }
    }
}
