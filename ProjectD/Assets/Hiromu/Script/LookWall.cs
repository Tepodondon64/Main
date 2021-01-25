using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWall : MonoBehaviour {

    public GameObject targetObject; // 注視したいオブジェクトを事前にInspectorから入れておく

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(targetObject.transform);
	}
    void OnTriggerStay(Collider collider)
    {

        //if (collision.gameObject.CompareTag("Untagged"))
        //{}
        if (collider.gameObject.GetComponent<Renderer>() == true)
        {
            collider.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {


        if (collider.gameObject.GetComponent<Renderer>() == true)
        {
            collider.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
