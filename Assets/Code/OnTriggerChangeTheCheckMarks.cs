using UnityEngine;

public class OnTriggerChangeTheCheckMarks : MonoBehaviour
{

    //"Global" Variables
    public bool InTriggerZone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    /*
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            InTriggerZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "player")
        {
            InTriggerZone = false;
        }

    }

}
    
