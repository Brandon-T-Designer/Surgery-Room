using JetBrains.Annotations;
using UnityEngine;

public class Body_Spawner : MonoBehaviour    
{

    public GameObject body;

    private float timer = 0;
    public double spawnRate = 0.8;
    


    public bool Game_Over = false;
    public double Body_Spanwer_Location_x;
    public int Body_Count = 0;
    public int Max_Body_Count = 5;

    private GameObject Clone;
    public Transform All_Bodies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SpawnBody() {

        Instantiate(body, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, All_Bodies);
        //Clone.transform.parent = All_Bodies;
    }

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Body_Spanwer_Location_x = transform.position.x;

        if (timer < spawnRate && Game_Over == false)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            if (Body_Count < Max_Body_Count)
            {
                SpawnBody();
                Debug.Log("Body Spawned");
                Body_Count = Body_Count + 1;
                Debug.Log("Body Count: "+Body_Count);
            }
            timer = 0;
        }
    }
}
