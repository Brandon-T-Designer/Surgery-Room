using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    float vertical, horizontal;
    Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall(); 
    }

    void MoveBall()
    {
        horizontal = Input.GetAxis("Horizontal");
        //Insert Animation Code

        vertical = Input.GetAxis("Vertical");
        //Insert Animation Code

        //Debug.Log(horizontal);
        myRigidbody2D.velocity = new Vector2(horizontal*moveSpeed, vertical * moveSpeed);
    }
}
