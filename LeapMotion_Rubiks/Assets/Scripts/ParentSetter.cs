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
    public ColorMatchDetector[] colorMatchDetector;
    public float rotationAmount;
    public bool horizontal;
    public static bool rotating;

    private void Start()
    {
        colorMatchDetector = FindObjectsOfType<ColorMatchDetector>();
        cubePiece = FindObjectsOfType<Piece>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(rotateKey) && !rotating)
        {
            rotating = true;
            //SetPiecesToCore();
            //Rotate();
            StartCoroutine(RotatePieces());
            StartCoroutine(UnParent());
        }

        if (!rotating)
        {
            core = null;
            pieces.Clear();
        }
    }

    IEnumerator RotatePieces()
    {
        yield return new WaitForSeconds(0.1f);
        SetPiecesToCore();
        Rotate();
        //DetectColor();
    }

    IEnumerator UnParent()
    {
        yield return new WaitForSeconds(0.4f);
        UnParentPieces();
        //DetectColor();
    }

    void Rotate()
    {
        if (core != null && !rubikRotation.isRotating)
        {
            if (!horizontal)
            {
                //core.transform.Rotate(Vector3.forward, 90f, Space.World);
                LeanTween.rotateAround(core, Vector3.forward, 90f, 0.2f);
            }
            else
            {
                LeanTween.rotateAround(core, Vector3.up, 90f, .2f);
                //core.transform.Rotate(Vector3.up, 90f, Space.World);
            }
        }
    }

    void DetectColor()
    {
        foreach (ColorMatchDetector colorMatchDetector in colorMatchDetector)
        {
            colorMatchDetector.CheckMatchingColors();
        }
    }

    IEnumerator DetectMatchedColors()
    {
        yield return new WaitForSeconds(1f);
        foreach (Piece piece in cubePiece)
        {
            piece.DetectMatchingColors();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (rotating)
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
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //UnParentPieces();
    }

    public void UnParentPieces()
    {
        foreach (var piece in pieces)
        {
            piece.transform.SetParent(rubikParent);
        }
        rotating = false;
        // multiCores = false;
        //StartCoroutine(ClearList());
    }

    IEnumerator ClearList()
    {
        yield return new WaitForSeconds(0.7f);
      
    }


    void SetPiecesToCore()
    {
        foreach (var piece in pieces)
        {
            piece.transform.SetParent(core.transform);
        }
    }
}
