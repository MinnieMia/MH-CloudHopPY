using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUMP : MonoBehaviour
{
float jumpForce = 15;

public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
       rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if(GetComponent<Rigidbody>().velocity.y > -.01 && GetComponent<Rigidbody>().velocity.y < .01)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
