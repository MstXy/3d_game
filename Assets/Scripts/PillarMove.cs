using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PillarMove : MonoBehaviour
{
    [SerializeField] private Material matGrey;
    [SerializeField] private Material matYellow;

    [SerializeField] private OpenDoor OpenDoorScript1;
    [SerializeField] private OpenDoor OpenDoorScript2;

    public GameObject[] allPillars;

    private bool[,] matrix = new bool[4, 4] { { false, false, false, false }, 
                                                { false, false, false, false }, 
                                                { false, false, false, false }, 
                                                { false, false, false, false } };

    // Start is called before the first frame update
    void Start()
    {
        allPillars = GameObject.FindGameObjectsWithTag("Pillar");
        // correct pillars index is 12, 31
        // randomize two pillars
        
        // pillar one 
        int xCor_1 = Random.Range(0, 4);
        int yCor_1 = Random.Range(0, 4);
        
        // change if initial is already correct
        while (xCor_1 == 1 && yCor_1 == 2)
        {
            xCor_1 = Random.Range(0, 4);
            yCor_1 = Random.Range(0, 4);
        }
        
        int xCor_2 = Random.Range(0, 4);
        int yCor_2 = Random.Range(0, 4);

        // change if overlap or not 棋盘格
        while (xCor_2 == xCor_1 && yCor_2 == yCor_1 || xCor_2 == 1 && yCor_2 == 2)
        {
            xCor_2 = Random.Range(0, 4);
            yCor_2 = Random.Range(0, 4);
        }
        
        matrix[xCor_1,yCor_1] = true;
        matrix[xCor_2,yCor_2] = true;
        // assign texture:
        GameObject.Find("Pillar_" + xCor_1 + yCor_1).GetComponent<Renderer>().material = matGrey;
        GameObject.Find("Pillar_" + xCor_2 + yCor_2).GetComponent<Renderer>().material = matGrey;


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pillar"))
        {
            Debug.Log(other.gameObject.name);

            int thisX = (int)Char.GetNumericValue(other.gameObject.name[7]);
            int thisY = (int)Char.GetNumericValue(other.gameObject.name[8]);
            matrix[thisX, thisY] = !matrix[thisX, thisY];

                
            // update pillar material:
            if (matrix[thisX, thisY])
            {
                GameObject.Find("Pillar_" + thisX + thisY).GetComponent<Renderer>().material = matGrey;

            } else {
                GameObject.Find("Pillar_" + thisX + thisY).GetComponent<Renderer>().material = matYellow;
            }
            
            // check if match
            bool is_match = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((i == 1 && j == 2 || i == 3 && j == 1))
                    {
                        is_match = is_match && matrix[i, j] == true;
                    }
                    else
                    {
                        is_match = is_match && matrix[i, j] == false;
                    }
                }
            }
            
            if (is_match)
            {
                OpenDoorScript1.puzzleSolved = true;
                OpenDoorScript2.puzzleSolved = true;
            }
            else
            {
                OpenDoorScript1.puzzleSolved = false;
                OpenDoorScript2.puzzleSolved = false;
            }
        }
    }
}
