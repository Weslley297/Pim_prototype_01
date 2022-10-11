using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public Animator anim;

    private NavMeshAgent agent;
    private GameObject target;
    private Vector3 movement = new Vector3();
    private float delay = 0;
    private bool isBloqued = false;   
    private bool movingToX;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;

        target = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D  collision) 
    {
        isBloqued = true;
    }

    void OnCollisionExit2D(Collision2D  collision)
    {
        isBloqued = false;
        delay = 0;
    }

    void FixedUpdate()
    {
        agent.SetDestination(target.transform.position);

        delay += Time.deltaTime;
        if(delay < 0.1){
            return;
        }

        var nextPosition = transform.position + movement;
        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime);  
        if(delay < 0.4){
            return;
        }
        
        var distance = agent.nextPosition - transform.position;
        movement = getLinearMovement(distance);

        this.setStateParameters();
        delay = 0;
    }

    private Vector3 getLinearMovement(Vector3 distance){
        float movementDistance = 0;
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        var moveToX = Math.Abs(distance.x) > Math.Abs(distance.y);

        movingToX = moveToX;
        if(movingToX == !isBloqued)
        {
            movementDistance = distance.x < 0 ? -moveSpeed : moveSpeed;
            return new Vector3(movementDistance, 0, 0);
        }

        movementDistance = distance.y < 0 ? -moveSpeed : moveSpeed;
        return new Vector3(0, movementDistance, 0);
    }

    private void setStateParameters(){
        anim.SetFloat("DirX", movement.x);
        anim.SetFloat("DirY", movement.y);
    }
}