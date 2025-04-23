using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera FollowCamera;
    public void AssignCamera(Transform target)
    {
        FollowCamera.Follow = target;
        FollowCamera.LookAt = target;
    }
    private void Update()
    {
        OnMouseScroll();
    }
    public void OnMouseScroll()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            FollowCamera.m_Lens.OrthographicSize -= 0.1f;  
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            FollowCamera.m_Lens.OrthographicSize += 0.1f;
        }
    }
}
