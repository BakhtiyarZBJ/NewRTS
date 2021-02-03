using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage;
    public Root target;
    public bool isHeal;

    public float vel, acel, force;

    	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, vel * Time.deltaTime);
            vel += acel * Time.deltaTime;
            acel += force * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                if (isHeal)
                {
                    target.GetHeals(damage);
                }
                else
                {
                    target.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
	}
}
