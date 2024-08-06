using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Move_Body : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = 100;

    public int Body_Count;
    public int Max_Body_Count;
    //public double Final_Body_Pos = 5;
    //public double Body_Spanwer_Location_x;
    public GameObject TreatmentIcons;

    //"Global" Variables
    public int ProcedureNumber;
    public bool SurgeryTableOccupied;
    public bool BodiesStoppedMoving = false;
    public GameObject cutPathIndicatorsParentGo;
    public BoxCollider2D cutStartZoneBoxCollider;
    public BoxCollider2D cutEndZoneBoxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ProcedureNumber = Random.Range(1, 4);
        //Debug.Log(ProcedureNumber);
        //Final_Body_Pos = 5;
        //BodiesStoppedMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        //"Global Variables"
        SurgeryTableOccupied = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().SurgeryTableOccupied;

        //Other Variables
        Body_Count = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Body_Count;
        Max_Body_Count = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Max_Body_Count;

        /*
        Body_Spanwer_Location_x = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Body_Spanwer_Location_x;

        double Spawn_Range = Final_Body_Pos - Body_Spanwer_Location_x;
        double Stop_Body_Here =  (Final_Body_Pos - ((Spawn_Range/Max_Body_Count)*(Body_Count)));
        */

        if (Body_Count < Max_Body_Count)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed)*Time.deltaTime;
        }
        else 
        {
            GameObject.Find("All_Bodies").GetComponent<FinalMoveBodiesScript>().SetAllBodiesHaveSpawned(true);
        }
        
        /*
        if (transform.position.x < Final_Body_Pos)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }
        */

        if (transform.position.x > deadZone)
        {
            Destroy(gameObject);
            Debug.Log("Body Deleted");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        BodiesStoppedMoving = GameObject.Find("All_Bodies").GetComponent<FinalMoveBodiesScript>().BodiesStoppedMoving;
        //Update If Condtion
        if ((BodiesStoppedMoving == true) && (SurgeryTableOccupied == false))
        {
            CutBodyMinigame.body = this;
            transform.position = GameObject.Find("SurgeryTable").transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.position = transform.position + Vector3.up;

            TreatmentIcons.GetComponent<CounterRotateCanvas>().CounterRotateTheCanvas();

            //Update Global Variable Command Center
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().SetProcedureNumber(ProcedureNumber);

            //Now Occupy The Surgery Table
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().SetSurgeryTableOccupied(true);

        }
    }

    public void SetProcedureNumber(int NewProcedureNumber) 
    {  
        ProcedureNumber = NewProcedureNumber;
        Debug.Log(ProcedureNumber);
    }
}
