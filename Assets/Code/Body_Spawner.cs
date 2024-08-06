using JetBrains.Annotations;
using UnityEngine;

public class Body_Spawner : MonoBehaviour    
{

    public GameObject LiverTransplant;
    public GameObject Appendicitis;
    public GameObject GallstoneDisease;
    public int ProcedureNumber;

    private float timer = 0;
    public double spawnRate = 0.8;
    


    public bool Game_Over = false;
    public double Body_Spanwer_Location_x;
    public int Body_Count = 0;
    public int Max_Body_Count = 5;

    private GameObject Clone;
    public Transform All_Bodies;
    public bool BodiesStoppedMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
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
                //Debug.Log("Body Spawned");
                Body_Count = Body_Count + 1;
                //Debug.Log("Body Count: "+ Body_Count);
            }
            else 
            { 
                //Trigger Movement of All Bodies
            
            }
            timer = 0;
        }        
    }
    void SpawnBody()
    {
        //Decided which Procedure the patient needs
        ProcedureNumber = Random.Range(1, 4);

        //Creates the proper prefab associated with the procedure
        if (ProcedureNumber == 1)
        {
            //Liver Transplant
            Instantiate(LiverTransplant, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, All_Bodies);
        }
        else if (ProcedureNumber == 2)
        {
            //Appendicitis
            Instantiate(Appendicitis, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, All_Bodies);
        }
        else if (ProcedureNumber == 3)
        {
            //Gallstone Disease
            Instantiate(GallstoneDisease, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation, All_Bodies);
        }

        All_Bodies.transform.GetChild(Body_Count).GetComponent<Move_Body>().SetProcedureNumber(ProcedureNumber);
        //Clone.transform.parent = All_Bodies;
    }
}
