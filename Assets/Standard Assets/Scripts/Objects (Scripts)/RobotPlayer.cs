using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RobotPlayer : MonoBehaviour
{
    public float moveSpeed;
    public Transform trs;
    public Rigidbody2D rigid;
    public BoxCollider2D medicationBenchBoxCollider;
    Vector2 destination;

    void Start ()
    {
        destination = trs.position;
    }

    void Update ()
    {
        HandleMovement ();
    }

    void HandleMovement ()
    {
        Vector2 move = destination - (Vector2) trs.position;
        move.Normalize();
        rigid.velocity = move * moveSpeed;
    }

    public void GrabDrugA ()
    {
        destination = medicationBenchBoxCollider.ClosestPoint(trs.position);
    }
}
