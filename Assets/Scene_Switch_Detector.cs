using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Switch_Detector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Level_1_Exit") ) {
             SceneManager.LoadScene("Scene 2");
             Debug.Log("Success");
        }
    }
}
