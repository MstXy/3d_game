using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showInstruction : MonoBehaviour
{
    [SerializeField] private TMP_Text gameInstruction;

    private bool near;
    // Start is called before the first frame update
    void Start()
    {
        
    }
// on collision with the doors, show UI constantly
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            near = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameInstruction.gameObject.SetActive(false);
            near = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (near)
        {
            gameInstruction.gameObject.SetActive(true);
            gameInstruction.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0,1,0));
        }
    }
}
