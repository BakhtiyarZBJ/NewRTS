using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAttack : MonoBehaviour {

    public Root myRoot, targetRoot;
    public List<Root> enemies;
    public bool atkReady;
    public NavMeshAgent myAgent;

    public GameObject bullet;
    public float range;

    public float rotateSpeedWhenAtk = 8;
    float velocityRoto;

	// Use this for initialization
	void Start () {
        myRoot = GetComponent<Root>();
        myAgent = GetComponent<NavMeshAgent>();
        Invoke("ReadyAction", myRoot.atkSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        enemies.Clear();
        foreach(Root detectionObject in myRoot.detected)
        {
            if (detectionObject != null)
            {
                if (detectionObject.tag == "Orc") enemies.Add(detectionObject);
            }
        }

        if (enemies.Count > 0)
        {
            targetRoot = enemies[0];
            foreach(var enemy in enemies)
            {
                float dist1 = Vector3.Distance(transform.position, targetRoot.transform.position);

                float dist2 = Vector3.Distance(transform.position, enemy.transform.position);

                if (dist1 > dist2)
                {
                    targetRoot = enemy;
                }
            }

            if (myRoot.currentState != Root.STATE.Moving)
            {
                myRoot.ChangeState(Root.STATE.Combat);

                if (Vector3.Distance(transform.position, targetRoot.transform.position) > range)
                {
                    myAgent.SetDestination(targetRoot.transform.position);
                    myAgent.Resume();
                }
                else
                {
                    myAgent.Stop();

                    myAgent.Stop();
                    Quaternion rotationToLook = Quaternion.LookRotation(targetRoot.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref velocityRoto, rotateSpeedWhenAtk * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    if (atkReady)
                    {
                        atkReady = false;
                        Invoke("ReadyAction", myRoot.atkSpeed);

                        int dmg = myRoot.atk + Random.Range(-myRoot.atkVar, myRoot.atkVar + 1);

                        GameObject missileClone = Instantiate(bullet, transform.position, Quaternion.identity);//
                        missileClone.GetComponent<Bullet>().damage = dmg;
                        missileClone.GetComponent<Bullet>().target = targetRoot;
                    }
                }
            }
        }
        else if (myRoot.currentState == Root.STATE.Combat)
        {

        }
	}
    void ReadyAction()
    {
        atkReady = true;
    }
}
