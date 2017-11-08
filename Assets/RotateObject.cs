using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotSpeed = 10f;
    private Quaternion targetRotation;

    void Start ()
    {
        targetRotation = transform.rotation;
    }

    void Update ()
    {
        transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    public void Rotate (float degree, Vector3 rotationAxis)
    {
        targetRotation *= Quaternion.AngleAxis (degree, rotationAxis);
    }
}
