using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Controller : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("enter");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("stay");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("exit");
        }
    }
}
