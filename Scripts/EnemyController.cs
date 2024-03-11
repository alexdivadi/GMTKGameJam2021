using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 1f;
    public float acceleration = 1f;
    public float rotationAcceleration = 0.5f;
    public float circlingDistance = 4f;
    public float range = 8f;
    public float friction = 0.5f;
    public float changeDirectionTimer = 10f;
    public enum Behavior {
        RAM,
        FLEE,
        CIRCLE
    };
    public Behavior behavior;
    public bool isBoss = false;

    private int lateralDirection;
    private float elapsedTime;
    private Collider2D[] objectsInRange;
    private float distanceToPlayer;
    private GameObject enemyDot;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        objectsInRange = new Collider2D[100];
        lateralDirection = 1;
        if (isBoss)
        {
            enemyDot = Resources.Load("Prefabs/BossDot") as GameObject;
            Instantiate(enemyDot, gameObject.transform);
        }
        else
        {
            enemyDot = Resources.Load("Prefabs/EnemyDot") as GameObject;
            Instantiate(enemyDot, gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= changeDirectionTimer)
        {
            lateralDirection *= -1;
            elapsedTime = 0;

            GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
        }

        Array.Clear(objectsInRange, 0, 100);
        Physics2D.OverlapCircleNonAlloc(transform.position, range, objectsInRange);

        if (objectsInRange != null && Array.Exists(objectsInRange, x => (x != null && x.gameObject.tag == "Player")))
        {

            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);     

            Vector2 playerDirection = player.transform.position - transform.position;
            Vector2 forward = Vector3.Normalize(playerDirection);

            if (behavior == Behavior.CIRCLE)
            {
                Vector2 left = Vector2.Perpendicular(forward);
                transform.up = Vector3.Lerp(transform.up, playerDirection, Time.deltaTime * rotationAcceleration);

                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToPlayer > circlingDistance)
                {
                    GetComponent<Rigidbody2D>().velocity += forward * acceleration * Time.deltaTime;
                }
                else if (distanceToPlayer < circlingDistance)
                {
                    GetComponent<Rigidbody2D>().velocity -= forward * acceleration * Time.deltaTime;
                }

                GetComponent<Rigidbody2D>().velocity += lateralDirection * left * acceleration * Time.deltaTime;

            }
            else if (behavior == Behavior.RAM)
            {
                transform.up = Vector3.Lerp(transform.up, playerDirection, Time.deltaTime * rotationAcceleration);

                GetComponent<Rigidbody2D>().velocity += forward * acceleration * Time.deltaTime;
            }
            else if (behavior == Behavior.FLEE)
            {
                playerDirection = transform.position - player.transform.position;
                forward = Vector3.Normalize(playerDirection);
                transform.up = Vector3.Lerp(transform.up, playerDirection, Time.deltaTime * rotationAcceleration);
                GetComponent<Rigidbody2D>().velocity += forward * acceleration * Time.deltaTime;
            }

        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0f)
        {
            Vector3 normal = Vector3.Normalize(GetComponent<Rigidbody2D>().velocity);
            Vector2 back = new Vector2(normal.x, normal.y);

            GetComponent<Rigidbody2D>().velocity += -back * friction * Time.deltaTime;
        }
    }

}
