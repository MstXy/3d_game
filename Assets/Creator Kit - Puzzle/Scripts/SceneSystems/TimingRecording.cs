using System;
using System.Collections;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingRecording : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera cam;
    public KeyCode resetKeyCode = KeyCode.R;
    public Rigidbody startingMarble;
    // public SceneCompletion sceneCompletion;
    public TextMeshProUGUI textMesh;
    public Action enableControlAction;
    [HideInInspector]
    public float timer;

    
    
    [SerializeField] private OpenDoor03 OpenDoorScript;

    bool m_IsTiming;
    BaseInteractivePuzzlePiece[] m_PuzzlePieces;

    void Awake ()
    {
        m_PuzzlePieces = FindObjectsOfType<BaseInteractivePuzzlePiece> ();
        
        enableControlAction = EnableControl;
    }

    void EnableControl ()
    {
        startingMarble.isKinematic = false;
        m_IsTiming = true;
        for (int i = 0; i < m_PuzzlePieces.Length; i++)
        {
            m_PuzzlePieces[i].EnableControl ();
        }
    }

    void Update ()
    {
        if(m_IsTiming)
            timer += Time.deltaTime;

        textMesh.text = timer.ToString ("0.00");

        if (Input.GetKeyDown(resetKeyCode))
            // sceneCompletion.ReloadLevel ();
            SceneManager.LoadSceneAsync("Level03");
    }

    public void GoalReached (float uiDelay)
    {
        m_IsTiming = false;
        StartCoroutine (CompleteLevelWithDelay (uiDelay));
    }

    IEnumerator CompleteLevelWithDelay (float delay)
    {
        yield return new WaitForSeconds (delay);
        // sceneCompletion.CompleteLevel (timer);
        Debug.Log("win");
        OpenDoorScript.puzzleSolved = true;
        player.transform.localScale = new Vector3((float)0.5, (float)0.5, (float)0.5);
        player.GetComponent<ThirdPersonController>().JumpHeight /= (float)2;
        // player.GetComponent<ThirdPersonController>().MoveSpeed /= (float)2;
        // player.GetComponent<ThirdPersonController>().SprintSpeed /= (float)2;
        cam.m_Lens.FieldOfView = 10;
    }
}
