using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHit : MonoBehaviour {
    public float time;
    
    [HideInInspector]
    public GameObject origin;

    [HideInInspector]
    public GameObject target;

    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start() {
        lastPos = target.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        time -= Time.deltaTime;
        if (time <= 0f) {
            Destroy(gameObject);
        }

        if (target != null) {
            lastPos = target.transform.position;
        }

        stretch();
    }

    private void stretch() {
        Vector2 center = (origin.transform.position + lastPos) / 2f;
        transform.position = center;

        Vector2 direction = lastPos - origin.transform.position;
        transform.up = Vector2.Perpendicular(direction);

        Vector2 scale = Vector2.one;
        scale.x *= Vector2.Distance(lastPos, origin.transform.position) * 0.54f;
        transform.localScale = scale;
    }
}
