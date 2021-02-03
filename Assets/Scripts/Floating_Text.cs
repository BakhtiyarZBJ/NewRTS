using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Text : MonoBehaviour {

    public string Dano;
    public TextMesh myTextMesh;
	// Use this for initialization
	void Start () {
        myTextMesh.text = Dano;
        myTextMesh.transform.eulerAngles = Camera.main.transform.eulerAngles;
	}
	
}
