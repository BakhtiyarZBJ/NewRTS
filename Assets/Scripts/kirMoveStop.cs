using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kirMoveStop: MonoBehaviour
{
    public float rotoSpeed;
    public kirMovement kirMovement;
    public SpriteRenderer cursorSprite;

    void Start()
    {
        cursorSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotoSpeed * Time.deltaTime);
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == kirMovement.tag)
        {
            if (kirMovement.myRoot.currentState==Root.STATE.Moving)
            {
                kirMovement.myRoot.ChangeState(Root.STATE.Idle);
                kirMovement.agent.Stop();
                cursorSprite.enabled = false;
            }
            
        }
    }
}
