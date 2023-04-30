using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
    public string exitName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetString("ChosenScene") == sceneToLoad)
        {
            PlayerPrefs.SetString("LastExitName",exitName);
            // reset chosen scene
            PlayerPrefs.SetString("ChosenScene", null);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
