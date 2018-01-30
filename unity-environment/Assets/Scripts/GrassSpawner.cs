using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[System.Serializable]
public class GrassSpawnerEvent : UnityEvent<GrassSpawner>
{
}

[Serializable]
public class GrassSpawner : MonoBehaviour
{
	[Header("Objects")]
	[SerializeField] private GameObject ClodOfGrassPrefab;
	[SerializeField] private Transform GrassFolder;
	[SerializeField] private GameObject rockPrefab;
	[SerializeField] private Transform rockFolder;

	[Header("Parameters")]
	[SerializeField] private Rect area;
	[SerializeField] private int rockNumber = 2;
	[SerializeField] private float clodDistance = 1f;
	[SerializeField] private Vector3 randomRange = new Vector3(.2f, 0, .2f);

	[Header("Events")]
	public GrassSpawnerEvent OnClodMowned = new GrassSpawnerEvent();

	[Header("Preview")]
	[SerializeField] private bool showSpawns;
	
	private List<GameObject> ClodOfGrasses = new List<GameObject>();
	private List<GameObject> rocks = new List<GameObject>();
	private Vector3[] spawnCache;
	private int clodCount = 0;
	private int clodMownedCount = 0;

	public int ClodCount {get { return clodCount; }}
	public int ClodMownedCount {get { return clodMownedCount; }}

	private void Awake ()
	{
		SpawnArea ();
	}
	
	private void Update ()
	{
		
	}

	private void OnValidate()
	{
		spawnCache = SpawnPositions();
	}

	private void OnDrawGizmosSelected()
	{
		GizmosExtended.DrawRect(area, true, Color.green);
		
		if (!showSpawns)
			return;
		
		if (spawnCache == null)
		{
			spawnCache = SpawnPositions();
		}

		GizmosExtended.RetainColorAndChangeTo(Color.green);
		foreach (Vector3 spawn in spawnCache)
		{
			Gizmos.DrawWireSphere(spawn, .15f);
		}
		GizmosExtended.RestoreColorToRetained();
	}

	private Vector3[] SpawnPositions()
	{
		int columns = Mathf.FloorToInt(area.width / clodDistance);
		int rows = Mathf.FloorToInt(area.height / clodDistance);

		float xOffset = (area.width - clodDistance * columns) / 2f + (clodDistance / 2f);
		float yOffset = (area.height - clodDistance * rows) / 2f + (clodDistance / 2f);
		
		var spawns = new List<Vector3>();

		for (var y = 0; y < rows; y++)
		{
			for (var x = 0; x < columns; x++)
			{
				var potentialSpawn = new Vector3(
					area.xMin + xOffset + (x * clodDistance) + Random.Range(-randomRange.x, randomRange.x),
					Random.Range(-randomRange.y, randomRange.y),
					area.yMin + yOffset + (y * clodDistance) + Random.Range(-randomRange.z, randomRange.z)
					);
				if (!SpawnIsInGrassObstacle(potentialSpawn))
				{
					spawns.Add(potentialSpawn);
				}
			}
		}

		return spawns.ToArray();
	}

	private bool SpawnIsInGrassObstacle(Vector3 point)
	{
		foreach (GameObject rock in rocks)
		{
			if (rock.GetComponent<GrassObstacle>().ContainPoint(point))
			{
				return true;
			}
		}
		return false;
	}

	private void OnClodMownedEvent()
	{
		clodMownedCount++;
		OnClodMowned.Invoke(this);
	}
	
	private void OnClodResetEvent()
	{
		clodMownedCount--;
	}

	public void ResetAllClodOfGrass()
	{
		foreach (GameObject clodOfGrass in ClodOfGrasses)
		{
			clodOfGrass.GetComponent<ClodOfGrass>().Mowned = false;
		}
		clodMownedCount = 0;
	}

	public void SetArea(Rect value) {
		area = value;
		spawnCache = null;
	}

	private void SpawnRocks()
	{
		foreach (GameObject rock in rocks) {
			Destroy (rock);
		}
		rocks.Clear();
		
		// Instanciate rocks
		for (int i = 0; i < rockNumber; i++)
		{
			rocks.Add(Instantiate(rockPrefab, Vector3.zero, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), rockFolder));
		}
		// Place rocks
		for (int j = 0; j < rocks.Count; j++)
		{
			var grassObstacleScript = rocks[j].GetComponent<GrassObstacle>();
			Vector3 point;
			var pointIsOk = false;
			do
			{
				// Take a random place
				point = new Vector3(
					Random.Range(area.xMin + grassObstacleScript.Radius, area.xMax - grassObstacleScript.Radius),
					0f,
					Random.Range(area.yMin + grassObstacleScript.Radius, area.yMax - grassObstacleScript.Radius)
				);
				pointIsOk = true;
				// Check all previous rocks distance
				for (int k = j - 1; k >= 0; k--)
				{
					// If the rocks are too close
					if (Vector3.Distance(rocks[k].transform.position, point) < rocks[k].GetComponent<GrassObstacle>().Radius + grassObstacleScript.Radius)
					{
						pointIsOk = false;
						break;
					}
				}
			} while (!pointIsOk);
			
			rocks[j].transform.position = point;
		}
	}

	public void SpawnArea()
	{
		SpawnRocks();
		
		spawnCache = SpawnPositions();
		foreach (GameObject clod in ClodOfGrasses) {
			Destroy (clod);
		}
		ClodOfGrasses.Clear();
		foreach (Vector3 spawn in spawnCache)
		{
			GameObject go = Instantiate(ClodOfGrassPrefab, spawn, Quaternion.identity, GrassFolder);
			go.GetComponent<ClodOfGrass>().OnMownedAction += OnClodMownedEvent;
			go.GetComponent<ClodOfGrass>().OnResetAction += OnClodResetEvent;
			ClodOfGrasses.Add(go);
		}
		clodCount = ClodOfGrasses.Count;
		clodMownedCount = 0;
	}
}
