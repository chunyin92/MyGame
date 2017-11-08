﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSprite : MonoBehaviour
{
    Transform cameraTransform;

    void Awake ()
    {
        cameraTransform = Camera.main.transform;        
    }

    void Update ()
    {
        transform.LookAt (transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }    
}
