using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainRubikRotation : MonoBehaviour
{
    private bool isRotating = false;
    private float rotationSpeed = 3.0f; // Adjust the speed as needed

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateObjectY());
        }
    }

    private IEnumerator RotateObjectY()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90f, transform.rotation.eulerAngles.z));
        float elapsedTime = 0f;
        while (elapsedTime < 1.0f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }
}