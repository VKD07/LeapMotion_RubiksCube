using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public float rayLength = 1.0f;
    public LayerMask cubePieceLayer;
    public bool colorDetected;
    private Renderer cubeRenderer;
    public int numberOfMatches;
    public ColorMatch colorMatch;
    GameScore gameScore;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        colorMatch = FindObjectOfType<ColorMatch>();
        gameScore = FindObjectOfType<GameScore>();
       // DetectMatchingColors();
    }

    private void Update()
    {
    }

    public void DetectMatchingColors()
    {
        Color cubeColor = cubeRenderer.sharedMaterial.color;

        // Define directions for raycasting
        Vector3[] directions = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        foreach (Vector3 direction in directions)
        {
            // Cast a ray in each direction
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rayLength, cubePieceLayer))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

                if (hitRenderer != null)
                {
                    Color hitColor = hitRenderer.sharedMaterial.color;

                    // Check if the neighboring cube piece has the same color
                    if (hitColor == cubeColor)
                    {
                        numberOfMatches++;
                        Debug.Log("Matching color found!");
                        // Debug visualization: Draw the ray in the Scene view
                        Debug.DrawRay(transform.position, direction * rayLength, Color.green, 0.5f);

                        if (hit.collider.GetComponent<Piece>().numberOfMatches > 1)
                        {
                            gameScore.score++;
                            RandomizeColor(hit.collider.GetComponent<Renderer>());
                            RandomizeColor(cubeRenderer);
                        }
                    }
                    else
                    {
                        // Debug visualization: Draw the ray in the Scene view (non-matching color)
                        Debug.DrawRay(transform.position, direction * rayLength, Color.red, 0.5f);
                    }
                }
            }
            else
            {
                // Debug visualization: Draw the ray in the Scene view (no hit)
                Debug.DrawRay(transform.position, direction * rayLength, Color.yellow, 0.5f);
            }
        }
    }

    void RandomizeColor(Renderer renderer)
    {
        int randomColor = Random.Range(0, colorMatch.colors.Length);
        renderer.material = colorMatch.colors[randomColor];
    }
}
