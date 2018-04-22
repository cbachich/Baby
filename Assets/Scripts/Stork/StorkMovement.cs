using System;
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
	private Rigidbody2D m_Rigidbody;

	[SerializeField]
	private StorkInteract storkInteract;

	public event EventHandler StorkCollected;

	float maxX = 9.0f;
	float minX = -9.0f;
	float maxY = 6.0f;
	float minY = -6.0f;

	public FlightDirection FlightDirection { 
		get { return direction; } 
	}

	Vector3 flyingDirection;

	// Use this for initialization
	void Awake () {
		m_Rigidbody = GetComponent<Rigidbody2D>();
		SetDirection();
	}
	
	// Update is called once per frame
	void Update () {
		Fly();

		if (IsOutsideOfView()) {
			if (StorkCollected != null) {
				StorkCollected(this, EventArgs.Empty);
			}

			Destroy(gameObject);
		}
	}

	private void Fly() {
		m_Rigidbody.velocity = transform.up * speed;
	}

	private void SetDirection() {
		if (direction == FlightDirection.Down) {
			m_Rigidbody.MoveRotation(180);
		}
		else if (direction == FlightDirection.Right) {
			m_Rigidbody.MoveRotation(-90);
		}
		else if (direction == FlightDirection.Left) {
			m_Rigidbody.MoveRotation(90);
		}
	}

	private bool IsOutsideOfView()
	{
		return 
			transform.position.x > maxX 
			|| transform.position.x < minX 
			|| transform.position.y > maxY 
			|| transform.position.y < minY;
	}
}
