using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstSceneSolved : MonoBehaviour
{
    [SerializeField] private OpenDoor OpenDoorScript1;
    [SerializeField] private OpenDoor OpenDoorScript2;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator waiter()
    {
        //Wait for 1 second
        yield return new WaitForSeconds(1);
        
        OpenDoorScript1.puzzleSolved = true;
        OpenDoorScript2.puzzleSolved = true;
    }
}
