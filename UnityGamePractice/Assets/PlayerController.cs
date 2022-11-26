using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //tuple
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    SpriteRenderer spriteRenderer;
    bool canMove = true;
    public SwordAttack swordAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // FixedUpdate is called once per few seconds
    void FixedUpdate() {
        if(canMove){
            animator.SetBool("isHeMoving", false);
            if(movementInput != Vector2.zero){ 
                bool success = TryMove(movementInput);
                if(!success && movementInput.x > 0) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if(!success && movementInput.y > 0) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isHeMoving", success);
            } else {
                animator.SetBool("isHeMoving", false);
            }

            // Set direction of sprite to movement direction
            if(movementInput.x < 0){
                spriteRenderer.flipX = true;
            } else if(movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction){
        int count = rb.Cast(
            movementInput, // (x, y) telling us the direction
            movementFilter, // tells us where a collision can occur
            castCollisions, // list of collisions to store the found collisions into
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // the amount to cast equal to the movement plus offset
        if(count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    public void SwordAtt(){
        lockMovement();
        if(spriteRenderer.flipX == true){
            swordAttack.attackLeft();
        } else {
            swordAttack.attackRight();
        }
    }

    public void lockMovement(){
        canMove = false;
    }   

    public void unlockMovement(){
        canMove = true;
    }


}
