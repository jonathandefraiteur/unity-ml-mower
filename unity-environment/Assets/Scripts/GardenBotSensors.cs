using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Events;
using Mower;

[Serializable]
// Sensor group label, Sensor label, Hits 
public class RaycastHitSensorEvent : UnityEvent<string, string, RaycastHit[]>
{
}

public class GardenBotSensors : MonoBehaviour
{

	[SerializeField] private RaySensorGroup[] raySensorGroups = new []{ new RaySensorGroup("Default", Color.cyan, Vector3.zero, new RaySensor[0], Physics.AllLayers) };
	public int[] lastValues = new int[8];


	[Header("Events")]
	public RaycastHitSensorEvent SensorEvent;
	
	private void Start ()
	{
		
	}

	private void OnDrawGizmosSelected()
	{
		foreach (RaySensorGroup raySensorGroup in raySensorGroups)
		{
			foreach (RaySensor raySensor in raySensorGroup.GlobalRaySensors())
			{
				GizmosExtended.RetainColorAndChangeTo(raySensorGroup.Color);
				
				RaySensor raySensorGlobal = raySensor.ToWorldSpace(transform);
				Gizmos.DrawLine(raySensorGlobal.Origin, raySensorGlobal.GetEndPoint());
				
				GizmosExtended.RestoreColorToRetained();
			}
		}
	}

	public RaySensorGroup GetSensorGroup(string groupLabel)
	{
		return raySensorGroups.FirstOrDefault(raySensorGroup => raySensorGroup.Label == groupLabel);
	}

	public RaySensorHitGroup[] PlaySensors ()
	{
		var hitGroups = new List<RaySensorHitGroup>();
		foreach (RaySensorGroup raySensorGroup in raySensorGroups)
		{
			hitGroups.Add(new RaySensorHitGroup(raySensorGroup.Label, PlaySensors(raySensorGroup.Label)));
		}
		return hitGroups.ToArray();
	}

	public RaySensorHit[] PlaySensors(string groupLabel)
	{
		RaySensorGroup sensorGroup = GetSensorGroup(groupLabel);
		return sensorGroup != null ? PlaySensors(sensorGroup) : null;
	}
	
	public RaySensorHit[] PlaySensors(RaySensorGroup raySensorGroup)
	{
		var raySensorHits = new List<RaySensorHit>();
		foreach (RaySensor raySensor in raySensorGroup.GlobalRaySensors())
		{
			RaySensor raySensorGlobal = raySensor.ToWorldSpace(transform);
			RaycastHit[] hits = Physics.RaycastAll(raySensorGlobal.ToRay(), raySensorGlobal.MaxDistance, raySensorGroup.LayerMask, raySensorGroup.QueryTriggerInteraction);

			if (hits.Length > 0)
			{
				SensorEvent.Invoke(raySensorGroup.Label, raySensor.Label, hits);
			}

			raySensorHits.Add(new RaySensorHit(raySensor.Label, hits));
		}
		return raySensorHits.ToArray();
	}
}
