using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GardenBotMove : MonoBehaviour
{
	[SerializeField] private float maxVelocity = 5f;
	[SerializeField] private float maxAngleVelocity = 20f;
	
	private new Rigidbody rigidbody;
	
	private void Start ()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	private void OnDisable()
	{
		if (rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();
		
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	public void Move(float acceleration, float rotation)
	{
		acceleration = Mathf.Clamp (acceleration, -1f, 1f);
		rotation = Mathf.Clamp (rotation, -1f, 1f);

		rigidbody.velocity = transform.forward * acceleration * maxVelocity;
		rigidbody.angularVelocity = transform.up * rotation * maxAngleVelocity;
	}
}
