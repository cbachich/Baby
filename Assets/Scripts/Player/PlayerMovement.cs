using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
    private float speed = 5.0f;

	void Update () {
		var x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        var y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, y, 0);
	}
}
