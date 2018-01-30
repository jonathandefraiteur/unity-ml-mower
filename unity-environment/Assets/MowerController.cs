using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using Mower;
using UnityEngine;

public class MowerController : MonoBehaviour
{
	[SerializeField] private bool drivenByAgent = false;
	[Header("Inputs")]
	[SerializeField] public string AccelerationAxe = "Vertical";
	[SerializeField] public string RotationAxe = "Horizontal";

	private GardenBotMove gardenBotMove;
	private GardenBotSensors gardenBotSensors;
	private MowerBlade mowerBlade;
	
	private void Start ()
	{
		gardenBotMove = GetComponent<GardenBotMove>();
		gardenBotSensors = GetComponent<GardenBotSensors>();
		mowerBlade = GetComponent<MowerBlade>();
	}
	
	private void Update ()
	{
		if (drivenByAgent)
			return;
		
		if (gardenBotMove != null)
			gardenBotMove.Move (Input.GetAxis(AccelerationAxe), Input.GetAxis(RotationAxe));
		
		if (gardenBotSensors != null)
			gardenBotSensors.PlaySensors();
		
		if (mowerBlade != null)
			mowerBlade.MowGrass();
	}
}
