using UnityEngine;

public class Move_Body : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = 100;

    public int Body_Count;
    public int Max_Body_Count;
    public double Final_Body_Pos;
    public double Body_Spanwer_Location_x; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Final_Body_Pos = 5;

    }

    // Update is called once per frame
    void Update()
    {
        
        Body_Count = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Body_Count;
        Max_Body_Count = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Max_Body_Count;

        /*
        Body_Spanwer_Location_x = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Body_Spanwer_Location_x;

        double Spawn_Range = Final_Body_Pos - Body_Spanwer_Location_x;
        double Stop_Body_Here =  (Final_Body_Pos - ((Spawn_Range/Max_Body_Count)*(Body_Count)));
        */



        /*
        if (Body_Count < Max_Body_Count) 
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }
        */


        if (transform.position.x < Final_Body_Pos)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }

        if (transform.position.x > deadZone)
        {
            Destroy(gameObject);
            Debug.Log("Body Deleted");
        }
    }
}
