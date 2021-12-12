using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject impactFX;
 
    private bool collided;
    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag != "Bullet" && co.gameObject.tag != "Player" && !collided) 
        {
            collided = true;
            var impact = Instantiate(impactFX, co.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 2);
            Destroy(gameObject);
            //print("collided" + collided);
        }
    }
}
