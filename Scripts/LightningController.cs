using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LightningController : MonoBehaviour {
    public float range;
    public float fireRate;
    public int damage;

    public GameObject projectile;
    public GameObject firePoint;

    private Collider2D[] objectsInRange;
    private float curTime;

    // Start is called before the first frame update
    void Start() {
        objectsInRange = new Collider2D[100];
        curTime = fireRate;
    }

    // Update is called once per frame
    void FixedUpdate() {
        curTime -= Time.deltaTime;
        if (gameObject.tag == "Stuck" && curTime <= 0f) {
            Array.Clear(objectsInRange, 0, 100);
            Physics2D.OverlapCircleNonAlloc(transform.position, range, objectsInRange);

            Collider2D closestTarget = null;

            foreach (Collider2D col in objectsInRange) {
                if (col == null) {
                    break;
                }

                if (col.gameObject.layer != 6 &&
                    col.gameObject.GetComponent<LifeCounter>() != null && 
                    col.gameObject.tag != "Projectile" && 
                    Vector2.Distance(transform.position, col.transform.position) <= range && 
                    GetComponent<LifeCounter>().team != col.gameObject.GetComponent<LifeCounter>().team && 
                    col.gameObject.GetComponent<LifeCounter>().team != "") {
                    if (closestTarget == null || Vector2.Distance(col.transform.position, firePoint.transform.position) < 
                        Vector2.Distance(closestTarget.transform.position, firePoint.transform.position)) {
                            closestTarget = col;
                    }
                }
            }

            if (closestTarget != null) {
                curTime = fireRate;
                fire(closestTarget);
            }
        }
    }
    

    private void fire(Collider2D target) {
        GameObject lightning = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
        lightning.GetComponent<LightningHit>().target = target.gameObject;
        lightning.GetComponent<LightningHit>().origin = firePoint;

        target.gameObject.GetComponent<LifeCounter>().lifeCounter--;

        AudioManager.instance.playSound("Thunder");
    }
}
