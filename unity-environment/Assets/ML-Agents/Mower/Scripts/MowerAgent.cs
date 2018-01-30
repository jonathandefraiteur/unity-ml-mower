using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mower;
using Mower.Scripts;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GardenBotMove))]
[RequireComponent(typeof(GardenBotSensors))]
[RequireComponent(typeof(MowerBlade))]
public class MowerAgent : Agent
{
	private Rigidbody rigidbody;
	private GardenBotMove gardenMoveScript;
	private GardenBotSensors gardenBotSensorsScript;
	private MowerBlade mowerBladeScript;
	[Header("References")]
	public GrassSpawner environment;
	public GameObject mowerBase;
	[Header("Parameters")]
	[SerializeField] private float chrono = 30f; // seconds
	[SerializeField][ReadOnly] private float startTime = 0f;
	[SerializeField] private LayerMask criticColliderMask;
	[Header("Rewards")]
	public float stepReward = -.001f;
	public float grassReward = .1f;
	public bool useGrassBoostReward = true;
	public float grassBoostReward = 10f;
	public float allMownReward = 1f;
	public float obstacleTouchedReward = -1f;
	[Header("Debug")]
	[ReadOnly] public float accelerationUse = 0f;
	[ReadOnly] public float rotationUse = 0f;
	[ReadOnly] public bool hasCollideWithObstacle = false;
	[ReadOnly] public float lastReward = 0f;
	public Text rewardText;
	[ReadOnly] public float totalReward = 0f;
	public Text rewardTotalText;


	private void Start ()
	{
		rigidbody = GetComponent<Rigidbody>();
		gardenMoveScript = GetComponent<GardenBotMove> ();
		gardenBotSensorsScript = GetComponent<GardenBotSensors> ();
		mowerBladeScript = GetComponent<MowerBlade> ();

		startTime = Time.time;
	}
		

	public override List<float> CollectState()
	{
		List<float> state = MowerAgentHelper.CreateState();
		
		RaySensorHitGroup[] raySensorHitGroups = gardenBotSensorsScript.PlaySensors();

		// For each Sensor Group
		foreach (RaySensorHitGroup raySensorHitGroup in raySensorHitGroups)
		{
			var raySensorHits = raySensorHitGroup.RaySensorHits.ToArray();
			// For each ray in the group
			foreach (RaySensorHit raySensorHit in raySensorHits)
			{
				// Find the closest hit
				float closest = 999f;
				foreach(RaycastHit hit in raySensorHit.Hits)
				{
					float distance = Vector3.Distance (hit.point, transform.position);
					if (distance < closest)
						closest = distance;
				}
				
				string baseLabel = raySensorHitGroup.Label + "_" + raySensorHit.Label;
				// Set the distance state
				MowerAgentHelper.SetState(baseLabel + "_Distance", state, closest);
				// If the group is "LongGrass"
				if (raySensorHitGroup.Label == "LongGrass")
				{
					MowerAgentHelper.SetState(baseLabel + "_Count", state, closest);
				}
			}
		}
		return state;
	}

	public override void AgentStep(float[] act)
	{
		gardenMoveScript.Move (MowerAgentHelper.GetAction("Acceleration", act), MowerAgentHelper.GetAction("Rotation", act));
		accelerationUse = MowerAgentHelper.GetAction ("Acceleration", act);
		rotationUse = MowerAgentHelper.GetAction ("Rotation", act);

		ClodOfGrass[] mownGrass = mowerBladeScript.MowGrass();
		reward += stepReward;

		float boost = useGrassBoostReward ? (environment.ClodMownedCount / environment.ClodCount) * grassBoostReward : 1;

		reward += mownGrass.Length * (grassReward * boost);

		// End of time
		if (startTime + chrono <= Time.time) {
			done = true;
		}
		// Obstacle touched
		else if (hasCollideWithObstacle) {
			reward += obstacleTouchedReward;
			done = true;
		}
		// All is mown
		else if (environment.ClodCount == environment.ClodMownedCount) {
			reward += allMownReward;
			done = true;
		}

		lastReward = reward;
		rewardText.text = lastReward.ToString(CultureInfo.InvariantCulture);
		totalReward += lastReward;
		rewardTotalText.text = totalReward.ToString(CultureInfo.InvariantCulture);
	}

	public override void AgentReset()
	{
		startTime = Time.time;
		hasCollideWithObstacle = false;
		transform.position = mowerBase.transform.position;
		transform.rotation = mowerBase.transform.rotation;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		totalReward = 0f;
		
		environment.SpawnArea ();
	}

	public override void AgentOnDone()
	{

	}

	private void OnCollisionEnter(Collision other)
	{
		// The object as is layer in the critick mask
		if (criticColliderMask == (criticColliderMask | (1 << other.gameObject.layer)))
		{
			hasCollideWithObstacle = true;
		}
	}
}
