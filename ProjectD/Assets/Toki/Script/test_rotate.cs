using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_rotate : MonoBehaviour {

    [SerializeField] private float angle;

    [SerializeField] private float speed;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        JOJO_RotateY(angle, speed);

        //Debug.Log(transform.rotation.y);
	}

    private void JOJO_RotateY(float y, float rotate2flame)
    {
        // 目的の角度を0 ～ 360に変換
        while (y >= 360)
        {
            y -= 360;
        }
        while (y < 0)
        {
            y += 360;
        }

        // 現在の角度を0 ～ 360に変換
        while (transform.rotation.y >= 360)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - 360, 0));
        }
        while (transform.position.y < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + 360, 0));
        }





        if (transform.rotation.y != y)
        {
            float reverse_angle = transform.rotation.y - 180;

            if (reverse_angle < 0)
            {
                reverse_angle += 360f;
            }

            //上にある
            if (transform.rotation.y < 180)
            {
                //180方向
                if (transform.rotation.y < y && reverse_angle >= y)
                {
                    if ((transform.rotation.y + rotate2flame) >= y)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + rotate2flame, 0));
                    }
                }
                //0方向
                else if ((transform.rotation.y > y && 0 <= y) || (reverse_angle < y && 360 > y))
                {
                    if (transform.rotation.y - rotate2flame < 0)
                    {
                        float range = transform.rotation.y - rotate2flame;

                        if (range < 0)
                        {
                            range += 360;
                        }

                        if ((transform.rotation.y > y && 0 <= y) || (range <= y && 360 > y))
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - rotate2flame, 0));
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
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - rotate2flame, 0));
                        }
                    }

                }


            }
            //下にある
            else
            {
                if (transform.rotation.y > y && reverse_angle <= y)
                {
                    if ((transform.rotation.y - rotate2flame) <= y)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y - rotate2flame, 0));
                    }
                }
                else if ((transform.rotation.y < y && 360 > y) || (reverse_angle > y && 0 <= y))
                {
                    if (transform.rotation.y + rotate2flame >= 360)
                    {
                        float range = transform.rotation.y + rotate2flame;

                        if (range >= 360)
                        {
                            range -= 360;
                        }

                        if ((transform.rotation.y < y && 360 > y) || (range >= y && 0 <= y))
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + rotate2flame, 0));
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
                            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + rotate2flame, 0));
                        }
                    }

                }

            }


        }
    }
}
