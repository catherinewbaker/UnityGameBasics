using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float Health{
        set {
            health = value;
            if(health <= 0){
                Defeated();
            }
        }
        get {
            return health;
        }

    }
    public void takeDamage(float damage){
        health -= damage;
    }

    public void Defeated(){
        Destroy(gameObject);
    }

}
