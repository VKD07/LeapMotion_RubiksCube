using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLocalRotation = transform.localRotation.eulerAngles;
        currentLocalRotation.y = 90f;
        currentLocalRotation.z = -90f;
    }
}
