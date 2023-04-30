using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    public GameObject permanentObjects;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(permanentObjects);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(permanentObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
