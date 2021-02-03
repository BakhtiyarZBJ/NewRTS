using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Transform kir1, kir2;
    public float speed;
    public MoveInput2 moveInput;

    // Update is called once per frame
    void Update()
    {
        if (moveInput.selectedKir == MoveInput2.SELECTED.kir1)
        {
            transform.position = Vector3.Lerp(transform.position, kir1.position, speed * Time.deltaTime);
        }
        if (moveInput.selectedKir == MoveInput2.SELECTED.kir2)
        {
            transform.position = Vector3.Lerp(transform.position, kir2.position, speed * Time.deltaTime);
        }

    }
}
