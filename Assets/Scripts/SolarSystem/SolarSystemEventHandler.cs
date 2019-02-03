using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemEventHandler : DefaultTrackableEventHandler
{
    public Camera arCamera;

    protected override void Start()
    {
        base.Start();
        arCamera.clearFlags = CameraClearFlags.SolidColor;
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        // Enable SkyBox
       // arCamera.clearFlags = CameraClearFlags.Skybox;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();        
        // Disable SkyBox
        //arCamera.clearFlags = CameraClearFlags.SolidColor;
    }
}
