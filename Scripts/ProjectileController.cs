using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float speed = 1;
    public string parentTeam;
    public int damage = 1;
    public float lifeTimer = 1.5f;

    public string sound;

    // Start is called before the first frame update
    void Start() {

    }

    void Update() {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.Translate(0, speed, 0);
    }
}
