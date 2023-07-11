using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform character;
    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

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

        // Calculate the vertical offset based on character's elevation
        float verticalOffset = Mathf.Max(character.position.y - minY, 0f);

        // Adjust the target position vertically
        float targetY = transform.position.y + verticalOffset;

        // Clamp the target position within the specified bounds
        targetX = Mathf.Clamp(targetX, minX, maxX);
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Set the camera's position to the target position
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}