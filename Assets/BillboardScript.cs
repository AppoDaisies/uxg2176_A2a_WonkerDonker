using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Transform camTransform;

    Quaternion originalRotation;
    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        //transform.LookAt(camTransform, Vector3.up);
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
