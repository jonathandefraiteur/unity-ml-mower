using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClodOfGrass : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private GameObject longGrassModel;
	[SerializeField] private float longGrassFlattenedHeight = .1f;
	[SerializeField] private float longGrassFlatteningSpeed = 3f;
	[SerializeField] private float longGrassPushUpSpeed = .75f;
	[SerializeField] private int longGrassLayer = 0;
	[SerializeField] private GameObject mownGrassModel;
	[SerializeField] private int mownGrassLayer = 0;
	[Header("State")]
	[SerializeField] private bool flattened = false;
	[SerializeField] private bool mowned = false;

	public event Action OnMownedAction;
	public event Action OnFlattenedAction;
	public event Action OnResetAction;

	public bool Flattened
	{
		get { return flattened; }
		set { flattened = value; }
	}
	
	public bool Mowned
	{
		get { return mowned;}
		set
		{
			if (value == mowned)
				return;
	
			mowned = value;
			longGrassModel.SetActive(!mowned);
			mownGrassModel.SetActive(mowned);
			gameObject.layer = mowned ? mownGrassLayer : longGrassLayer;
			if (mowned && (OnMownedAction != null))
				OnMownedAction();
			else if (OnResetAction != null)
				OnResetAction();
		}
	}
	
	private void Start ()
	{
		longGrassModel.SetActive(!mowned);
		mownGrassModel.SetActive(mowned);
		gameObject.layer = mowned ? mownGrassLayer : longGrassLayer;
	}
	
	private void Update ()
	{
		if (mowned)
			return;
		
		if (flattened && longGrassModel.transform.localScale.y > longGrassFlattenedHeight)
		{
			Vector3 scale = longGrassModel.transform.localScale;
			scale.y = scale.y - longGrassFlatteningSpeed * Time.deltaTime;
			longGrassModel.transform.localScale = scale;
		}
		else if (!flattened && longGrassModel.transform.localScale.y < 1)
		{
			Vector3 scale = longGrassModel.transform.localScale;
			scale.y = scale.y + longGrassPushUpSpeed * Time.deltaTime;
			longGrassModel.transform.localScale = scale;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Flattened = true;
		if (other.CompareTag("Blade"))
		{
			Mowned = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Flattened = false;
	}
}
