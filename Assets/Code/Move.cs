using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    float vertical, horizontal;
    Rigidbody2D myRigidbody2D;
    public Animator PlayerAnimator;
    public bool movingRight;

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
        horizontal = Input.GetAxisRaw("Horizontal")*moveSpeed;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            transform.localScale = new Vector2(-.85f, .85f);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            transform.localScale = new Vector2(.85f, .85f);
        }
        PlayerAnimator.SetFloat("Horizontal", Mathf.Abs(horizontal));
              
        vertical = Input.GetAxisRaw("Vertical")*moveSpeed;
        PlayerAnimator.SetFloat("Vertical", Mathf.Abs(vertical));
        myRigidbody2D.velocity = new Vector2(horizontal*moveSpeed, vertical * moveSpeed);
    }
}
