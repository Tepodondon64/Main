using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range : MonoBehaviour
{

    private Enemy_Move enemy_script;

	// Use this for initialization
	void Start ()
    {
        enemy_script = transform.parent.GetComponent<Enemy_Move>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemy_script.Player = other.gameObject;
        }
    }
}
