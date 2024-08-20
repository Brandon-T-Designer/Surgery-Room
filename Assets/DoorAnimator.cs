using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public int BodyCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Patient"))
        {
            BodyCount++;
            GetComponent<Animator>().SetBool("StartDoorAnimation", true);
            Debug.Log("BodyCount"+ BodyCount);
            
            if (BodyCount == 1)
            {
                GetComponent<Animator>().SetBool("BodiesInTrigger", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Patient"))
        {
            BodyCount--;
            Debug.Log("BodyCountExit" + BodyCount);
            if (BodyCount == 0)
            {
                GetComponent<Animator>().SetBool("BodiesInTrigger", false);
            }
        }
    }
}
