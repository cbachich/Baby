using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BabyState {
	Sitting,
	Crawling,
	Carried
}

public class BabyMovement : MonoBehaviour {

	[SerializeField]
    private float speed = 1.0f;

	[SerializeField]
    private float crawlTime = 5.0f;

	[SerializeField]
    private float sitTime = 5.0f;

	[SerializeField]
	private BabyState babyState = BabyState.Sitting;
	
	float sittingFor = 0.0f;
	float crawlingFor = 0.0f;

	float maxX = 5.75f;
	float minX = -5.75f;
	float maxY = 3.82f;
	float minY = -3.82f;

	float xDirection;
	float yDirection;

	public BabyState BabyState { get { return babyState; } }

	public Renderer rend;

	public void WasPickedUp() {
		babyState = BabyState.Carried;
	}

	public void WasDropped() {
		rend.enabled = true;
		Sit();
	}

	void Start() {
		rend = GetComponent<Renderer>();
        rend.enabled = true;
		
		if (babyState == BabyState.Crawling)
		{
			StartCrawling();
		}
	}

	void Update () {
		if (babyState == BabyState.Crawling) {
			Crawl();

			if (WantsToSit()) {
				Sit();
			}
		}
		else if (babyState == BabyState.Sitting && WantsToCrawl()) {
			StartCrawling();
		}
		else if (babyState == BabyState.Carried) {
			rend.enabled = false;
		}
	}

	void Crawl() {
		if (transform.position.x < minX || transform.position.x > maxX) {
			xDirection = -xDirection;
		}

		if (transform.position.y < minY || transform.position.y > maxY) {
			yDirection = -yDirection;
		}

		Vector3 direction = new Vector3(xDirection, yDirection, 0f);

		if((xDirection < 0 && transform.localScale.x > 0) || (xDirection > 0 && transform.localScale.x < 0)) {
			var scale = transform.localScale;
			scale.x *= -1f;
			transform.localScale = scale;
		}

		transform.Translate(direction * Time.deltaTime);
	}

	void Sit() {
		babyState = BabyState.Sitting;
		sittingFor = 0.0f;
	}

	void StartCrawling() {
		babyState = BabyState.Crawling;
		crawlingFor = 0.0f;
		ChangeDirection();
	}

	void ChangeDirection() {
		xDirection = Random.Range(-1f, 1f) * speed;
		yDirection = Random.Range(-1f, 1f) * speed;
	}

	bool WantsToSit()
	{
		crawlingFor += Time.deltaTime;
		return crawlingFor > crawlTime;
	}

	bool WantsToCrawl()
	{
		sittingFor += Time.deltaTime;
		return sittingFor > sitTime;
	}

	public void HandleBeingEaten() {
		GameObject.Destroy(gameObject);
	}
}
