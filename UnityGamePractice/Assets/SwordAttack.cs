using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    Vector2 rightAttackOffset;
    Collider2D swordCollider;
    // Start is called before the first frame update
    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
        swordCollider.enabled = false;
        
    }

    public void attackRight(){
        swordCollider.enabled = true;
        transform.position = rightAttackOffset;
    }

    public void attackLeft(){
        swordCollider.enabled = true;
        transform.position = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void stopAttack(){
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy"){
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null){
                enemy.health -= damage;
            }
        }
    }
}
