using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class GrassObstacle : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float radius = .5f;
	[SerializeField] private Vector3 offset = Vector3.zero;

	public float Radius => radius;
	

	private void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	private void OnDrawGizmosSelected()
	{
		GizmosExtended.RetainColorAndChangeTo(Color.red);
		Gizmos.DrawWireSphere(GrassObstacleCenter(), radius);
		GizmosExtended.RestoreColorToRetained();
	}

	public Vector3 GrassObstacleCenter()
	{
		return transform.TransformPoint(offset);
	}

	public bool ContainPoint(Vector3 point)
	{
		return Vector3.Distance(GrassObstacleCenter(), point) <= radius;
	}
}
