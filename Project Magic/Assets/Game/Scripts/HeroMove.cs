using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public float speed = 1f;

    void Update()
    {
        if (Input.GetKeyDown(up))
        {
            this.transform.position += new Vector3(0, speed * 1, 0);
        }
        if (Input.GetKeyDown(down))
        {
            this.transform.position -= new Vector3(0, speed * 1, 0);
        }
        if (Input.GetKeyDown(left))
        {
            this.transform.position -= new Vector3(speed * 1, 0, 0);
        }
        if (Input.GetKeyDown(right))
        {
            this.transform.position += new Vector3(speed * 1, 0 , 0);
        }
    }
}
