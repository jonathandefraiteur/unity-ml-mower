using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class GameEvent : UnityEvent<GameManager>
{
}

[Serializable]
public class GameManager : MonoBehaviour
{
	[Header("References")]
	public GrassSpawner GrassSpawner;
	public MowerController MowerController;
	public GameObject GardenBotStation;
	public Text MinutesText;
	public Text SecondesText;

	[Header("Parameters")]
	public float Chrono = 3 * 60;

	[Header("State")]
	public bool IsRunning = false;
	public float TimeLeft = 0f;
	public bool IsGameEnded = false;
	
	[Header("Events")]
	public GameEvent OnGameStart = new GameEvent();
	public GameEvent OnGameEnd = new GameEvent();
	
	void Start ()
	{
		Init();
	}
	
	void Update ()
	{
		if (IsRunning)
		{
			TimeLeft -= Time.deltaTime;
			if (TimeLeft <= 0)
			{
				IsRunning = false;
				TimeLeft = 0;
				if (OnGameEnd != null)
					OnGameEnd.Invoke(this);
				MowerController.enabled = false;
				IsGameEnded = true;
			}
			PrintChrono();
		}
		
		if (!IsRunning && !IsGameEnded)
		{
			// Start
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis(MowerController.AccelerationAxe) > 0)
			{
				IsRunning = true;
				MowerController.enabled = true;
				if (OnGameStart != null)
					OnGameStart.Invoke(this);
			}
		}
		else
		{
			// Reset
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				Init();
			}
		}
	}

	private void Init()
	{
		IsRunning = false;
		IsGameEnded = false;
		TimeLeft = Chrono;
		PrintChrono();
		
		MowerController.enabled = false;
		MowerController.transform.position = GardenBotStation.transform.position;
		MowerController.transform.rotation = GardenBotStation.transform.rotation;
		
		GrassSpawner.ResetAllClodOfGrass();
	}

	private void PrintChrono()
	{
		MinutesText.text = Mathf.FloorToInt(TimeLeft / 60).ToString();
		string secondes = Mathf.FloorToInt(TimeLeft % 60).ToString();
		if (secondes.Length < 2)
			secondes = '0' + secondes;
		SecondesText.text = secondes;
	}
}
