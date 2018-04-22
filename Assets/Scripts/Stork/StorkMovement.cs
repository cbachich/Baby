using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlightDirection {
	Up,
	Down,
	Right,
	Left
}

public class StorkMovement : MonoBehaviour {

	[SerializeField]
    private float speed = 1.0f;

	[SerializeField]
    private FlightDirection direction = FlightDirection.Up;

	[SerializeField]
	private Rigidbody m_Rigidbody;

	public FlightDirection FlightDirection { 
		get { return direction; } 
	}

	Vector3 flyingDirection;

	// Use this for initialization
	void Awake () {
		m_Rigidbody = GetComponent<Rigidbody>();
		SetDirection();
	}
	
	// Update is called once per frame
	void Update () {
		Fly();
	}

	private void Fly() {
		m_Rigidbody.velocity = transform.up * speed;
	}

	private void SetDirection() {
		if (direction == FlightDirection.Down) {
			transform.Rotate (Vector3.forward * 180);
		}
		else if (direction == FlightDirection.Right) {
			transform.Rotate (Vector3.forward * -90);
		}
		else if (direction == FlightDirection.Left) {
			transform.Rotate (Vector3.forward * 90);
		}
	}
}
