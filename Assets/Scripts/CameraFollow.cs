using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform character;
    public float minX;
    public float maxX;

    private void Update()
    {
        float targetX = character.position.x;
        targetX = Mathf.Clamp(targetX, minX, maxX);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
