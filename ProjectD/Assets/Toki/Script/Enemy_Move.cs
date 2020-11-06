using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject Player = null;

    [SerializeField] private float speed = 0;

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
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
        }

        rb.MovePosition(transform.position + transform.forward * speed);

        Player = null;
    }
}
