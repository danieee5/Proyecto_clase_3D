using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    // Public variable to control the base speed, adjustable from the editor.
    public float baseSpeed = 10f;

    // Public variable to control the rotation speed, adjustable from the editor.
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);

        Vector3 forwardDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;

        transform.Translate(forwardDirection * baseSpeed * Time.deltaTime, Space.World);
    }
}