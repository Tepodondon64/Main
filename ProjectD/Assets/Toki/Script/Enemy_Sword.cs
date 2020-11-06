using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sword : MonoBehaviour
{
    private bool attack = false;

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
            transform.rotation = Quaternion.Euler(enemy.transform.right * 90);
        }

        else
        {
            transform.rotation = Quaternion.Euler(enemy.transform.right * 0);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            attack = true;
        }
    }
}
