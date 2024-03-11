using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float maxSpeed;
    public float acceleration;
    public float rotationAcceleration;
    public float friction;
    public float fireDelay;

    public GameObject projectile;
    public GameObject firePoint;

    private float speed;
    private float fireDelayMax;

    // Start is called before the first frame update
    void Start() {
        fireDelayMax = fireDelay;
    }

    void Update() {
        if (/*Input.GetMouseButton(0) &&*/ fireDelay <= 0f) {
            projectile.GetComponent<ProjectileController>().parentTeam = GetComponent<LifeCounter>().team;
            Instantiate(projectile, firePoint.transform.position, transform.rotation);

            fireDelay = fireDelayMax;
        } else {
            if (fireDelay > 0f) {
                fireDelay -= Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 forward = transform.up;
        forward = Vector3.Normalize(forward);

        if (Input.GetKey(KeyCode.W)) {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)) {
            GetComponent<Rigidbody2D>().velocity -= Vector2.up * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
            Vector2 left = Vector2.Perpendicular(forward);
            GetComponent<Rigidbody2D>().velocity += Vector2.left * acceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            Vector2 right = Vector3.Reflect(Vector2.Perpendicular(forward), Vector2.Perpendicular(forward));
            GetComponent<Rigidbody2D>().velocity += Vector2.right * acceleration * Time.deltaTime;
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed) {
            GetComponent<Rigidbody2D>().velocity = maxSpeed * Vector3.Normalize(GetComponent<Rigidbody2D>().velocity);
        }

        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0f) {
                Vector3 normal = Vector3.Normalize(GetComponent<Rigidbody2D>().velocity);
                Vector2 back = new Vector2(-normal.x, -normal.y);

                GetComponent<Rigidbody2D>().velocity += back * friction * Time.deltaTime;
        }

        Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.up = Vector3.Lerp(transform.up, mouseDirection, Time.deltaTime * rotationAcceleration);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Wall") {
            Vector2 back = new Vector2(transform.position.x, transform.position.y) - other.GetContact(0).point;
            transform.position = other.GetContact(0).point + (back * transform.lossyScale.y * 1.25f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
