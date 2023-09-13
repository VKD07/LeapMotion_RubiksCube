using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSetter : MonoBehaviour
{
    public Transform rubikParent;
    public KeyCode rotateKey;
    public GameObject core;
    public List<GameObject> pieces = new List<GameObject>();


    private void Update()
    {
        if (Input.GetKeyDown(rotateKey))
        {
            if (core != null)
            {
                // Get the current rotation of the core
                Vector3 currentRotation = core.transform.rotation.eulerAngles;

                // Modify the rotation values
                currentRotation.x = 0f;
                currentRotation.y = 0; // Set to a constant value
                currentRotation.z += 90f; // Set to a constant value

                // Apply the new rotation to the core
                core.transform.rotation = Quaternion.Euler(currentRotation);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Core")
        {
            core = other.gameObject;
        }

        if(other.tag == "Pieces")
        {
            pieces.Add(other.gameObject);
        }


        if(core != null)
        {
            SetPiecesToCore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var piece in pieces)
        {
            piece.transform.SetParent(rubikParent);
        }
        core = null; ;
        pieces.Clear();
    }

    void SetPiecesToCore()
    {
        foreach(var piece in pieces)
        {
            piece.transform.SetParent(core.transform);
        }
    }
}
