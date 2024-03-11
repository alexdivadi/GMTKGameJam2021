using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    public float fireRate;
    public string sound;
    public GameObject firePoint;
    public GameObject projectile;

    private float timeToNextShot;
    private float height;
    private PolygonCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextShot = fireRate;
        col = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToNextShot -= Time.deltaTime;
        if (timeToNextShot <= 0f && tag == "Stuck")
        {
            fire();
            timeToNextShot = fireRate;
        }
    }

    private void fire()
    {
        projectile.GetComponent<ProjectileController>().parentTeam = GetComponent<LifeCounter>().team;
        Instantiate(projectile, firePoint.transform.position, transform.rotation);
        AudioManager.instance.playSound(sound);
    }
}
