using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform character;
    public float minX;
    public float maxX;


    //private void Update()
    //{
    //    float targetX = character.position.x;
    //    float targetY = character.position.y;
    //    targetX = Mathf.Clamp(targetX, minX, maxX);
    //    targetY = Mathf.Clamp(targetY, minY, maxY);
    //    transform.position = new Vector3(targetX, targetY, transform.position.z);
    //}

    private void LateUpdate()
    {
        // Calculate the target position
        float targetX = character.position.x;

        // Clamp the target position within the specified bounds
        targetX = Mathf.Clamp(targetX, minX, maxX);


        // Set the camera's position to the target position
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}