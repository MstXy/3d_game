using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform player;
    public Transform reciever;
    public bool othersideFlipped = false;
    public bool thissideFlipped = false;

    private bool playerIsOverlapping = false; 
    private void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                // teleport
                float rotationDiff;
                if (thissideFlipped)
                {
                    // set destined rotation
                    rotationDiff = 0;
                    player.transform.rotation = Quaternion.Euler(reciever.rotation.x, reciever.rotation.y, reciever.rotation.z);
                    // reset camera rotation
                    Camera.main.transform.rotation = Quaternion.identity;

                }
                else
                {
                    // smooth transition, dynamic rotation
                    rotationDiff = Quaternion.Angle(transform.rotation, reciever.rotation);
                    rotationDiff += 180;
                    player.Rotate(Vector3.up, rotationDiff);
                }

                Vector3 flippedOffset = new Vector3(0,0,0);
                if (othersideFlipped)
                {
                    flippedOffset += new Vector3(1, 1, 1); // to the side a bit
                }
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer + flippedOffset;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}
