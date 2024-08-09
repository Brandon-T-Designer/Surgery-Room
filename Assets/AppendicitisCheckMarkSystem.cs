using UnityEngine;

public class AppendicitisCheckMarkSystem : MonoBehaviour
{
    ItemSlot[] itemSlots;
    bool LocalStartChangingTheCheckMarks = false;

    //Checkmarks
    //public GameObject Appendicitis_Prefab;
    public GameObject CefazolinCheckMark;

    //FUCK_GITHUB
    int FUCK_GITHUB = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        LocalStartChangingTheCheckMarks = GetComponent<Move_Body>().StartChangingTheCheckMarks;
        if (LocalStartChangingTheCheckMarks == true) 
        {
            SetCheckMarkActive("Cefazolin", CefazolinCheckMark);
        }
           
    }

    //TreatmentIcons Display Code 
    public void SetCheckMarkActive(string TargetItem, GameObject Checkmark)
    {
        bool StopLoop = false;
        itemSlots = GameObject.Find("GlobalVariables").GetComponent<GlobalVariableCommandCenter>().itemSlots;

        for (int i = 0; ((i < itemSlots.Length) && (StopLoop == false)); i++)
        {
            if (itemSlots[i].itemName == TargetItem)
            {
                Checkmark.SetActive(true);
                StopLoop = true;
            }
        }
    }

}
