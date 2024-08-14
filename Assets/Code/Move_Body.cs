using UnityEngine;
using UnityEngine.InputSystem;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Move_Body : MonoBehaviour
{
    //FUCK_GITHUB
    int FUCK_GITHUB = 0;

    float moveSpeed;
    float deadZone = 0;

    int Body_Count;
    int Max_Body_Count;
    //public double Final_Body_Pos = 5;
    //public double Body_Spanwer_Location_x;
    public GameObject TreatmentIcons;

    //Check Mark System Control Variables
    public bool StartChangingTheCheckMarks = false;
    public SerializableDictionary<string, GameObject> requiredItemsForCheckmarksDict;
    public Collider2D interactRangeCollider;
    public Collider2D collider;
    public Transform trs;
    public static Move_Body currentlyTreating;

    //"Global" Variables
    int ProcedureNumber;
    bool StationIsOccupied;
    bool HasThisBodyBeenTreated = false;
    //bool BodiesStoppedMoving = false;

    //PostOpTables
    public GameObject Table1;
    public GameObject Table2;
    public GameObject Table3;
    public GameObject Table4;
    public GameObject Table5;

    //PostOpVariables
    public bool Table1Occupied = false;
    public bool Table2Occupied = false;
    public bool Table3Occupied = false;
    public bool Table4Occupied = false;
    public bool Table5Occupied = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Deactivate
        moveSpeed = GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().moveSpeed;
        Max_Body_Count = GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().Max_Body_Count;
        deadZone = GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().deadZone;
        //ProcedureNumber = Random.Range(1, 4);
        //Debug.Log(ProcedureNumber);
        //Final_Body_Pos = 5;
        //BodiesStoppedMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Deactivate

        if (transform.position.x < deadZone)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }

        if (transform.position.x > deadZone && transform.position.y > 4)
        {
            Destroy(gameObject);
            GameObject.Find("GameLoseCanvas").GetComponent<GameLoseScript>().LoseTheGame();
            Debug.Log("Body Deleted");
        }
        //End

        /*
        Body_Spanwer_Location_x = GameObject.Find("Body_Spawner").GetComponent<Body_Spawner>().Body_Spanwer_Location_x;

        double Spawn_Range = Final_Body_Pos - Body_Spanwer_Location_x;
        double Stop_Body_Here =  (Final_Body_Pos - ((Spawn_Range/Max_Body_Count)*(Body_Count)));
        */

        /*
        if (transform.position.x < Final_Body_Pos)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }
        */

        /*
        Body_Count = GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().Body_Count;
        Debug.Log("Body_Count is" + Body_Count);
        if (Body_Count < Max_Body_Count)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed)*Time.deltaTime;
        }
        else 
        {
            GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().SetAllBodiesHaveSpawned(true);
            Debug.Log("Move_Body: NOT MOVING ANYMORE");
        } 
        */




        if ((Move.instance.trs.position - trs.position).sqrMagnitude < interactRangeCollider.bounds.extents.x * interactRangeCollider.bounds.extents.x && Grabbable.currentGrabbed != null && Mouse.current.leftButton.wasReleasedThisFrame && collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
        {
            for (int i = 0; i < requiredItemsForCheckmarksDict.Count; i ++)
            {
                string requiredItem = requiredItemsForCheckmarksDict.keys[i];
                if (requiredItem == Grabbable.currentGrabbed.id)
                {
                    ItemSlot itemSlot = Grabbable.currentGrabbed.GetComponentInParent<ItemSlot>();
                    itemSlot.RemoveItemFromSlot (itemSlot.itemName, itemSlot.quantity, itemSlot.itemSprite, itemSlot.itemDescription);
                    requiredItemsForCheckmarksDict.values[i].SetActive(true);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //"Global Variables"
        StationIsOccupied = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().StationIsOccupied;
        //BodiesStoppedMoving = GameObject.Find("All_Bodies").GetComponent<Body_Spawner>().BodiesStoppedMoving;

        //Update If Condtion
        if (/*(BodiesStoppedMoving == true) &&*/ (StationIsOccupied == false))
        {

            //Start Changing The Check Mark indicators Now.
            moveSpeed = 0;
            StartChangingTheCheckMarks = true;

            //Check for Surgery Table Treatments
            if (ProcedureNumber == 1 || ProcedureNumber == 2)
            {
                transform.position = GameObject.Find("SurgeryTable").transform.position;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.position = transform.position + Vector3.up;

                TreatmentIcons.GetComponent<CounterRotateCanvas>().CounterRotateTheCanvas();
            }
            else
            {
                //Check for Blood Station Treatments
                if (ProcedureNumber == 3)
                {
                    transform.position = GameObject.Find("BloodStation").transform.position;
                    transform.position = transform.position + 3.3f * Vector3.right;
                }
            }


            //Update Global Variable Command Center
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().SetProcedureNumber(ProcedureNumber);

            //Now Occupy The Surgery Table
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().SetStationIsOccupied(true);
            interactRangeCollider.enabled = true;
            currentlyTreating = this;
        }
    }

    public void SetProcedureNumber(int NewProcedureNumber)
    {
        ProcedureNumber = NewProcedureNumber;
        Debug.Log(ProcedureNumber);
    }

    public void CompleteProcedure()
    {

        HasThisBodyBeenTreated = true;
        StationIsOccupied = false;
        //Debug.Log(HasThisBodyBeenTreated);

        //RotateCanvasBack
        transform.rotation = Quaternion.Euler(0, 0, 90);
        TreatmentIcons.GetComponent<CounterRotateCanvas>().CounterRotateTheCanvas();

        MoveToPostOp();
    }


    public void MoveToPostOp()
    {
        if (Table1Occupied == false)
        {
            transform.position = Table1.transform.position;
        }
        else if (Table2Occupied == false)
        {
            transform.position = Table2.transform.position;
        }
        else if (Table3Occupied == false)
        {
            transform.position = Table2.transform.position;
        }
        else if (Table4Occupied == false)
        {
            transform.position = Table4.transform.position;
        }
        else if (Table5Occupied == false)
        {
            transform.position = Table5.transform.position;
        }
        else
        {
            //ActivateWinScreen
        }

    }
}