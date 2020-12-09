using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_Enemy : MonoBehaviour
{

    private Rigidbody rb;

    [HideInInspector] public GameObject Player = null;

    [SerializeField] private float speed = 0;

    public float enemy_state = 1;   //0:存在しない, 1:アイドル, 2:歩行, 3:横歩行, 4:攻撃, 5:起き上がり

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        E_Move();

        SetAnimState();
    }


    void E_Move()
    {
        if (Player != null)
        {

            Vector3 middle = transform.position - Player.transform.position;

            float angle = Mathf.Atan2(middle.x, middle.z) * Mathf.Rad2Deg;



            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));


            enemy_state = 2;
            rb.MovePosition(transform.position + transform.forward * speed);
        }

        else
        {
            enemy_state = 1;
        }

    }


    private void SetAnimState()
    {
        anim.SetFloat("State", enemy_state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            Destroy(gameObject);
        }
    }
}
