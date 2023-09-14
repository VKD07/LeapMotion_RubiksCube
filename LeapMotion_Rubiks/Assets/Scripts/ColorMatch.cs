using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour
{
    public Material[] colors; 
    public GameObject[] cubePieces;
    public float closeDistanceThreshold = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        //InitCubeColors();
    }

    private void InitCubeColors()
    {
        for (int i = 0; i < cubePieces.Length; i++)
        {
            int randomColor = UnityEngine.Random.Range(0,colors.Length);
            cubePieces[i].GetComponent<Renderer>().material = colors[randomColor];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CheckForClosePieces();
    }


    void CheckForClosePieces()
    {
        for (int i = 0; i < cubePieces.Length; i++)
        {
            for (int j = i + 1; j < cubePieces.Length; j++)
            {
                float distance = Vector3.Distance(cubePieces[i].transform.position, cubePieces[j].transform.position);

                // Check if the distance is less than or equal to the threshold
                if (distance <= closeDistanceThreshold)
                {
                    // Check if the materials are the same
                    Renderer rendererI = cubePieces[i].GetComponent<Renderer>();
                    Renderer rendererJ = cubePieces[j].GetComponent<Renderer>();

                    if (rendererI != null && rendererJ != null && rendererI.sharedMaterial == rendererJ.sharedMaterial)
                    {
                        int matchingCount = 2; // Start with 2 pieces

                        // Check for additional close pieces with the same material
                        for (int k = 0; k < cubePieces.Length; k++)
                        {
                            if (k != i && k != j)
                            {
                                float distanceToI = Vector3.Distance(cubePieces[i].transform.position, cubePieces[k].transform.position);
                                float distanceToJ = Vector3.Distance(cubePieces[j].transform.position, cubePieces[k].transform.position);

                                Renderer rendererK = cubePieces[k].GetComponent<Renderer>();

                                if (distanceToI <= closeDistanceThreshold && distanceToJ <= closeDistanceThreshold &&
                                    rendererI.sharedMaterial == rendererK.sharedMaterial && rendererJ.sharedMaterial == rendererK.sharedMaterial)
                                {
                                    matchingCount++;
                                    if (matchingCount >= 4)
                                    {
                                        Debug.Log("Found 4 close pieces with the same material!");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("No 4 close pieces with the same material found.");
    }
}
