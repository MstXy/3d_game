using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OpenDoor : MonoBehaviour
{
    [SerializeField] private TMP_Text doorInteractUI;
    [SerializeField] private string nextSceneName;

    [System.NonSerialized] public bool puzzleSolved;
    private bool nearThisDoor = false;
    private Vector3 offset = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    private void Start()
    {
        // reset chosen scene
        PlayerPrefs.SetString("ChosenScene", null);
        puzzleSolved = false;
    }

    // on collision with the doors, show UI constantly
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearThisDoor = true;
        }
    }

    private void Update()
    {
        if (nearThisDoor && puzzleSolved)
        {
            // Debug.Log("show open door");
            doorInteractUI.gameObject.SetActive(true);
            doorInteractUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed.");
                PlayerPrefs.SetString("ChosenScene", nextSceneName);
                Debug.Log(PlayerPrefs.GetString("ChosenScene") );
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorInteractUI.gameObject.SetActive(false);
            nearThisDoor = false;
        }
    }
}
