using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveInput2 : MonoBehaviour
{
    public enum SELECTED { kir1, kir2 };
    public SELECTED selectedKir;

    public Transform kir1Pointer, kir2Pointer;
    public kirMovement kir1Movement, kir2Movement;
    public float minMovRange;
    public SpriteRenderer cursor1, cursor2;

    public Text k1, k2;

    public Root selectedKirRoot;
    // Update is called once per frame

    private void Start()
    {
        GetSelectedKirRoot();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.tag == "Terrarian")
                    {
                        Debug.DrawLine(ray.origin, hit.point);
                        switch (selectedKir)
                        {
                            case SELECTED.kir1:
                                //kir1Pointer.position = new Vector3(hit.point.x, kir1Pointer.position.y, hit.point.z);
                                MoveToPoint(ref kir1Pointer, ref cursor1, kir1Movement, hit);
                               /* if (Vector3.Distance(kir1Pointer.position, kir1Movement.transform.position) > minMovRange)
                                {
                                    kir1Movement.myRoot.ChangeState(Root.STATE.Moving);
                                    cursor1.enabled = true;
                                }*/
                                break;

                            case SELECTED.kir2:
                                MoveToPoint(ref kir2Pointer, ref cursor2, kir2Movement, hit);
                                /*kir2Pointer.position = new Vector3(hit.point.x, kir2Pointer.position.y, hit.point.z);

                                if (Vector3.Distance(kir2Pointer.position, kir2Movement.transform.position) > minMovRange)
                                {
                                    kir2Movement.myRoot.ChangeState(Root.STATE.Moving);
                                    cursor2.enabled = true;
                                }*/
                                break;
                        }

                        
                    }
                }
            }
        }
    
        
    }

    public void MoveToPoint(ref Transform pointer, ref SpriteRenderer cursor, kirMovement kirMove, RaycastHit hitVar)
    {
        pointer.position = new Vector3(hitVar.point.x, pointer.position.y, hitVar.point.z);

        if (Vector3.Distance(pointer.position, kirMove.transform.position) > minMovRange)
        {
            kirMove.myRoot.ChangeState(Root.STATE.Moving);
            cursor.enabled = true;
        }
    }

    public void Select1()
    {
        selectedKir = SELECTED.kir1;
        GetSelectedKirRoot();
        k1.color = Color.white;
        k2.color = Color.black;
    }
    public void Select2()
    {
        selectedKir = SELECTED.kir2;
        GetSelectedKirRoot();
        k1.color = Color.black;
        k2.color = Color.white;        
    }

    public void Follow1()
    {
        if (selectedKirRoot.tag != "Kir1")
        {
            selectedKirRoot.ChangeState(Root.STATE.Following);
            selectedKirRoot.gameObject.GetComponent<kirMovement>().kirFollowing = kir1Movement.transform;
            Select1();
        }
    }

    public void Follow2()
    {
        if (selectedKirRoot.tag != "Kir2")
        {
            selectedKirRoot.ChangeState(Root.STATE.Following);
            selectedKirRoot.gameObject.GetComponent<kirMovement>().kirFollowing = kir2Movement.transform;
            Select2();
        }
    }

    public void GetSelectedKirRoot()
    {
        switch (selectedKir)
        {
            case SELECTED.kir1:
                selectedKirRoot = GameObject.FindWithTag("Kir1").GetComponent<Root>();
                break;
            case SELECTED.kir2:
                selectedKirRoot = GameObject.FindWithTag("Kir2").GetComponent<Root>();
                break;
        }
    }
}
