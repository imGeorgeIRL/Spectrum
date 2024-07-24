using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNoah : MonoBehaviour
{
    public GameObject noahPrefab;
    public Transform spawnPoint;
    public GameObject austin;

    private Renderer noahRenderer;

    private Animator anim;
    private bool noahSpawned;
    private bool canMove;
    private float stoppingDistance = 3f;
    private float moveSpeed = 4f;
    private bool isMoving = false;

    private bool havingMeltdown;
    private string dayOfWeek;
    // Start is called before the first frame update
    void Start()
    {
        havingMeltdown = DialogueLua.GetVariable("havingMeltdown").asBool;
        dayOfWeek = DialogueLua.GetVariable("dayOfWeek").asString;
        noahRenderer = noahPrefab.GetComponent<Renderer>();
        anim = noahPrefab.GetComponent<Animator>();

        noahRenderer.sortingOrder = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.safeZoneActive && havingMeltdown && dayOfWeek == "Tuesday")
        {
            if (!noahSpawned)
            {
                StartCoroutine(SpawningNoah());
            }
        }
        if (canMove && noahSpawned)
        {
            WalkTowardsAustin();
        }

        if (!isMoving)
        {
            anim.Play("Noah_Idle_Left");
            //make noah sitting on the ground animation;
        }
    }

    private IEnumerator SpawningNoah()
    {
        noahSpawned = true;
        yield return new WaitForSeconds(4);
        noahRenderer.sortingOrder = 6;
        canMove = true;
    }

    private void WalkTowardsAustin()
    {
        Vector2 direction = austin.transform.position - noahPrefab.transform.position;

        direction.y = 0;

        float distance = direction.magnitude;
        if (distance > stoppingDistance)
        {
            anim.Play("Noah_Walk_Left");
            Vector2 movement = direction.normalized * moveSpeed * Time.deltaTime;
            movement.y = 0;

            noahPrefab.transform.position += (Vector3)movement;
            isMoving = true;
        }
        if (distance <= stoppingDistance)
        {
            isMoving = false;
            canMove = false;
            GameManager.triggerDialogue = true;
        }
    }
}
