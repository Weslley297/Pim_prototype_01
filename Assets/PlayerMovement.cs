using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float directionX = 0;
    public float directionY = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 movement;
    
    private readonly Vector3 LEFT = new Vector3(-1,1,1);
    private readonly Vector3 RIGHT = new Vector3(1,1,1);

    void Update()
    {
        movement = this.getLinearMovement();

        setStateParameters();      

        transform.localScale = getLeftRight();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private Vector2 getLinearMovement(){
        Vector2 move;
        move.x = Input.GetAxisRaw("Horizontal");
        if(move.x != 0){
            directionX = move.x;
            directionY = 0;

            move.y = 0;
            return move;
        }

        move.y = Input.GetAxisRaw("Vertical");
        if(move.y != 0){
            directionY = move.y;
            directionX = 0;
        }

        return move;
    }

    private void setStateParameters(){
        anim.SetFloat("DirX", directionX);
        anim.SetFloat("DirY", directionY);  
        anim.SetFloat("Speed", movement.sqrMagnitude);  
    }

    private Vector3 getLeftRight(){
        return directionX > 0 ? RIGHT : LEFT;
    }
    
}
