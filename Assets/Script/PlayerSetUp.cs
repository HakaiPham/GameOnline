using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetUpCamera()
    {
        if (Object.HasInputAuthority)
        {
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow!=null)
            {
                cameraFollow.AssignCamera(transform);
            }
        }
    }
}
