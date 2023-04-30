using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera camera_alt;

    public Material cameraMat_alt;
    public Camera camera;

    public Material cameraMat;
    // Start is called before the first frame update
    void Start()
    {
        // alt 
        if (camera_alt.targetTexture != null)
        {
            camera_alt.targetTexture.Release();
        }
        camera_alt.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat_alt.mainTexture = camera_alt.targetTexture;
        
        // 
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }
        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = camera.targetTexture;
    }
}
