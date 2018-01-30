using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDebugger : MonoBehaviour
{

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public void PrintRaycastHitSensorEvent(string groupLabel, string sensorLabel, RaycastHit[] hits)
	{
		Debug.Log(groupLabel + "/" + sensorLabel + " " + hits.Length + " hits");
	}
}
