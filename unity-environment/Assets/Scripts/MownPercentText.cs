using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MownPercentText : MonoBehaviour
{
	private Text text;

	private void Awake()
	{
		text = GetComponent<Text>();
	}
	
	void Update ()
	{
		
	}

	public void UpdatePercent(GrassSpawner spawner)
	{
		if (text == null)
			return;
		if (spawner.ClodCount != 0)
		{
			text.text = (int) (spawner.ClodMownedCount * 100 / spawner.ClodCount) + " %";
		}
		else
		{
			text.text = "0 %";
		}
	}
}
