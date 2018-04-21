using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
    private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * this.speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * this.speed;

        transform.Translate(x, y, 0);
	}
}
