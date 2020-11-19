using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    private Rigidbody rb;

    [HideInInspector] public GameObject Player = null;

    [SerializeField] private float speed = 0;

    [SerializeField] private float maai = 0f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        E_Move();
	}

    void E_Move()
    {
        if(Player != null)
        {

            Vector3 middle = Player.transform.position - transform.position;

            float angle = Mathf.Atan2(middle.x, middle.z) * Mathf.Rad2Deg;

            

            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }


        if(Player != null)
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if(distance <= maai)
            {
                rb.MovePosition(transform.position + transform.right * (speed / 4));
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * speed);
            }

        }

        

        Player = null;
    }
}
