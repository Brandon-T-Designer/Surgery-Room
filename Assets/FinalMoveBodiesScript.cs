using UnityEngine;

public class FinalMoveBodiesScript : MonoBehaviour
{

    public bool BodiesStoppedMoving = false;
    public float moveSpeed = 5;
    public double Final_Bodies_Pos = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BodiesStoppedMoving == true)
        {
            if (transform.position.x < Final_Bodies_Pos)
            {
                transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
            }
        }    
    }

    public void SetBodiesStoppedMoving( bool NewBodiesStoppedMoving) 
    {
        BodiesStoppedMoving = NewBodiesStoppedMoving;
    }
}
