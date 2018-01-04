using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	[SerializeField] private float speed = 1f;

	private float maxDistance = 25f;
	private float z;

	// Use this for initialization
	void Start ()
	{
		z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float newPosition = transform.position.z + speed * Time.deltaTime;
		if (newPosition > z + maxDistance)
		{
			newPosition = z - maxDistance;
		}
		transform.position = new Vector3(1, 1, newPosition);
	}
}
