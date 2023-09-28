using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatchDetector : MonoBehaviour
{
    public List<GameObject> pieces;
    public int numOfMatch;
    public Color colorToMatch;
    public ColorMatch colorMatch;
    private void Start()
    {
        StartCoroutine(GetColorMatch());
        //CheckMatchingColors();
    }

    IEnumerator GetColorMatch()
    {
        yield return new WaitForSeconds(0.1f);
        if (pieces.Count > 0)
        {
            colorToMatch = pieces[0].GetComponent<Renderer>().sharedMaterial.color;
        }
        CheckMatchingColors();
    }

    private void Update()
    {
        if (numOfMatch >= 3)
        {
            RandomizeColor();
            numOfMatch= 0;
        }
    }

    public void CheckMatchingColors()
    {
        numOfMatch = 0;

        if (pieces.Count > 0 && numOfMatch < 3)
        {
            colorToMatch = pieces[0].GetComponent<Renderer>().sharedMaterial.color;
            foreach (var piece in pieces)
            {
                if (colorToMatch == piece.GetComponent<Renderer>().sharedMaterial.color)
                {
                    numOfMatch++;
                }
            }
        }
    }

    void RandomizeColor()
    {
        foreach (var piece in pieces)
        {
            RandomizeColor(piece.GetComponent<Renderer>());
        }
    }

    void RandomizeColor(Renderer renderer)
    {
        int randomColor = UnityEngine.Random.Range(0, colorMatch.colors.Length);
        renderer.material = colorMatch.colors[randomColor];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pieces") || other.CompareTag("Core") || other.CompareTag("CenterCore"))
        {
            if (!pieces.Contains(other.gameObject))
            {
                pieces.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pieces") || other.CompareTag("Core") || other.CompareTag("CenterCore"))
        {
            pieces.Remove(other.gameObject);
        }
    }
}
