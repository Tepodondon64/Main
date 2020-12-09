using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject Player = null;

    [SerializeField] private float speed = 0;

    [SerializeField] private float maai = 0f;

    public float enemy_state = 1;   //0:存在しない, 1:アイドル, 2:歩行, 3:横歩行, 4:攻撃, 5:起き上がり

    private Animator anim;

    AnimatorClipInfo clipInfo;

    [SerializeField] private float attack_wait = 0.0f;
    private float save_attack_wait;

    [SerializeField] private float save_attack = 0.0f;
    private float save_save_attack;



	
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        save_attack_wait = attack_wait;
        save_save_attack = save_attack;
	}
	

	
	void Update ()
    {
        clipInfo = anim.GetCurrentAnimatorClipInfo(0)[0];

        if (enemy_state != 5 && enemy_state != 4)
        {
            E_Move();
        }

        if(enemy_state == 4)
        {
            E_Attack();
        }

        if(enemy_state == 5)
        {
            E_StandUP();
        }

        SetAnimState();
	}


    void E_Move()
    {

        enemy_state = 1;


        //お手製Lookat
        if(Player != null)
        {

            Vector3 middle = Player.transform.position - transform.position;

            float angle = Mathf.Atan2(middle.x, middle.z) * Mathf.Rad2Deg;

            

            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }

        //敵とプレイヤーの距離によって行動を変える
        if(Player != null)
        {
            //プレイヤーと敵の距離を計測
            float distance = Vector3.Distance(transform.position, Player.transform.position);


            //敵の攻撃間合いに入ってたら横移動しながらタイミングを見て攻撃
            if(distance <= maai)
            {

                enemy_state = 3;
               
                rb.MovePosition(transform.position + transform.right * (speed / 6));


                attack_wait -= Time.deltaTime;

                if (attack_wait <= 0.0f)
                {
                    attack_wait = save_attack_wait;

                    enemy_state = 4;
                }
                

            }
            //間合いから外れていたら近づいてくる
            else
            {
                enemy_state = 2;
                rb.MovePosition(transform.position + transform.forward * speed);
            }

        }
        
    }

    void E_Attack()
    {

        if (clipInfo.clip.name == "アーマチュア|attack")
        {

            save_attack -= Time.deltaTime;

            if (save_attack <= 0.0f)
            {
                rb.MovePosition(transform.position + transform.forward * speed);
            }
        }

        if (clipInfo.clip.name == "アーマチュア|standup")
        {
            enemy_state = 5;
            save_attack = save_save_attack;
            return;
        }

    }

    void E_StandUP()
    {
        if(clipInfo.clip.name == "アーマチュア|idle")
        {
            enemy_state = 1;
        }
    }


    private void SetAnimState()
    {
        var clipInfo = anim.GetCurrentAnimatorClipInfo(0)[0];

        Debug.Log(clipInfo.clip.name);
        anim.SetFloat("State", enemy_state);

    }
}
