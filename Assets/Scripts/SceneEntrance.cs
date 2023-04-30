using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string lastExitName;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            var newPos = transform.position;
            // Debug.Log(newPos);
            // Destroy(gameObject);
            // var newPos = new Vector3(0, 1, -5);
            PlayerScript.instance.transform.position = newPos;
            PlayerScript.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
