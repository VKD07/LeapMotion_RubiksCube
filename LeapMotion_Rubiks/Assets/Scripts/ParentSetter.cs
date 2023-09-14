using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSetter : MonoBehaviour
{
    public Transform rubikParent;
    public KeyCode rotateKey;
    public GameObject core;
    public List<GameObject> pieces = new List<GameObject>();
    //public MainRubikRotation mainRoa
    bool multiCores;
    public MainRubikRotation rubikRotation;
    Piece[] cubePiece;

    private void Start()
    {
        cubePiece = FindObjectsOfType<Piece>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(rotateKey))
        {
            Rotate();
        }

    }

    public void Rotate()
    {
        if (core != null && !rubikRotation.isRotating)
        {
            // Rotate the core around the world Z-axis by +90 degrees
            core.transform.Rotate(Vector3.forward, 90f, Space.World);

            StartCoroutine(DetectMatchedColors());
        }
    }

    IEnumerator DetectMatchedColors()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Piece piece in cubePiece)
        {
            piece.DetectMatchingColors();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CenterCore")
        {
            core = other.gameObject;
            multiCores = true;
        }
        else if (other.tag == "Core" && !multiCores)
        {
            core = other.gameObject;
        }

        if (multiCores)
        {
            if (other.tag == "Pieces" || other.tag == "Core")
            {
                if (!pieces.Contains(other.gameObject))
                {
                    pieces.Add(other.gameObject);
                }
            }
        }
        else
        {
            if (other.tag == "Pieces")
            {
                if (!pieces.Contains(other.gameObject))
                {
                    pieces.Add(other.gameObject);
                }
            }
        }


        if (core != null)
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
        // multiCores = false;
        core = null; ;
        pieces.Clear();
    }

    void SetPiecesToCore()
    {
        foreach (var piece in pieces)
        {
            piece.transform.SetParent(core.transform);
        }
    }
}
