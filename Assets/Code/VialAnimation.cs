using UnityEngine;

public class VialAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Vial;
    public GameObject BloodType_1;
    public GameObject BloodType_2;
    public GameObject BloodType_3;
    public GameObject BloodType_4;
    public GameObject BloodType_5;
    public GameObject BloodType_6;
    public GameObject BloodType_7;
    public GameObject BloodType_8;

    public void ActivateAnimations()
    {
        Debug.Log("Hello");
    }

    public void ShowTheResult()
    {
        int BloodType = Random.Range(1, 9);
        Debug.Log("BloodType is:" + BloodType);

        if (BloodType == 1)
        {
            BloodType_1.SetActive(true);
        }
        else if (BloodType == 2)
        {
            BloodType_2.SetActive(true);
        }
        else if (BloodType == 3)
        {
            BloodType_3.SetActive(true);
        }
        else if (BloodType == 4)
        {
            BloodType_4.SetActive(true);
        }
        else if (BloodType == 5)
        {
            BloodType_5.SetActive(true);
        }
        else if (BloodType == 6)
        {
            BloodType_6.SetActive(true);
        }
        else if (BloodType == 7)
        {
            BloodType_7.SetActive(true);
        }
        else if (BloodType == 8)
        {
            BloodType_8.SetActive(true);
        }
    }
}
