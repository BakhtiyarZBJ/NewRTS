using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {
    public Root myRoot;

	// Use this for initialization
	void Start () {
        myRoot = gameObject.GetComponentInParent<Root>();
	}
	
	// Update is called once per frame
	public void OnTriggerEnter(Collider other) {
		if(other.tag!="Terrain"&& other.tag != "obstaculo" && other.tag != "Untagged")
        {
            myRoot.detected.Add(other.GetComponent<Root>());
        }
	}

    public void OnTriggerExit(Collider other)
    {
        myRoot.detected.Remove(other.GetComponent<Root>());
    }
    void Update()
    {
        foreach(Root detectedObject in myRoot.detected.ToArray())
        {
            if(detectedObject == null)
            {
                myRoot.detected.Remove(detectedObject);
            }
            else if (detectedObject.HP <= 0)
            {
                myRoot.detected.Remove(detectedObject);
            }
        }
        /*
        if (myRoot.currentState != Root.STATE.Moving)
        {
            if (myRoot.enemies.Count > 0)
            {
                myRoot.ChangeState(Root.STATE.Combat);
            }
            else
            {
                if (myRoot.currentState == Root.STATE.Combat)
                {
                    myRoot.ChangeState(Root.STATE.Idle);
                }
            }
        }*/
    }
}
