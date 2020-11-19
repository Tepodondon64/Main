using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sword : MonoBehaviour
{
    private bool attack = false;

    private float anglex = 0.0f;

    private GameObject enemy;

	// Use this for initialization
	void Start ()
    {
        enemy = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(attack)
        {
            anglex += 5;
             
            if(anglex > 90f)
            {
                attack = false;
            }
        }

        else
        {
            if(anglex > 0)
            {
                anglex -= 5;
            }

        }

        transform.localRotation = Quaternion.Euler(anglex, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            attack = true;
        }
    }
}
