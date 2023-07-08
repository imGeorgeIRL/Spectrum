using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float initialXPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        initialXPos = transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.sensoryMetre += 10f;
        }
    }
    private void OnDestroy()
    {
        initialXPos -= transform.position.x;
    }
}
