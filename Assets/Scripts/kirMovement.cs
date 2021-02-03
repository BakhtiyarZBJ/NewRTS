using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class kirMovement : MonoBehaviour
{
    public Root myRoot;
    public NavMeshAgent agent;
    public Transform pointer, kirFollowing;
    public float followingDistance;
    public SpriteRenderer pointerSprite;


    private void Start()
    {
        myRoot = gameObject.GetComponent<Root>();
        pointerSprite = pointer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myRoot.currentState==Root.STATE.Moving)
        {
            agent.SetDestination(pointer.position);
            agent.Resume();
        }
        if(myRoot.currentState == Root.STATE.Following)
        {
            pointer.position = new Vector3(kirFollowing.position.x, pointer.position.y, kirFollowing.position.z);
            pointerSprite.enabled = true;

            if (Vector3.Distance(transform.position, kirFollowing.position) > followingDistance)
            {
                agent.SetDestination(kirFollowing.position);
                agent.Resume();
            }
            else
            {
                agent.Stop();
            }
        }

        if (myRoot.currentState != Root.STATE.Following && myRoot.currentState != Root.STATE.Moving)
        {
            pointerSprite.enabled = false;
        }

    }
}
