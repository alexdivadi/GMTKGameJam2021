using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyDodgeProjectiles : MonoBehaviour
{
    private Collider2D projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projectile = Physics2D.OverlapCircle(transform.position, 1.5f) ;


        if (projectile != null && projectile.gameObject.tag == "Projectile" && projectile.gameObject.GetComponent<ProjectileController>().parentTeam != gameObject.GetComponent<LifeCounter>().team)
        {
            UnityEngine.Debug.Log(projectile.gameObject.GetComponent<ProjectileController>().parentTeam +":"+ gameObject.GetComponent<LifeCounter>().team);
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);     

            Vector2 projectileDirection = transform.position - projectile.gameObject.transform.position;
            Vector2 forward = Vector3.Normalize(projectileDirection);
            Vector2 left = Vector2.Perpendicular(forward);
            transform.up = Vector3.Lerp(transform.up, projectileDirection, Time.deltaTime * gameObject.GetComponent<EnemyController>().rotationAcceleration);
            
            GetComponent<Rigidbody2D>().velocity += left * 10 * gameObject.GetComponent<EnemyController>().acceleration * Time.deltaTime;
        }
    }
}
