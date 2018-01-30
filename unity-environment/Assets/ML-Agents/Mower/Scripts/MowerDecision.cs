using System.Collections;
using System.Collections.Generic;
using Mower.Scripts;
using UnityEngine;

public class MowerDecision : MonoBehaviour, Decision
{

    public float[] Decide(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
    {
        float[] act = MowerAgentHelper.CreateAction();
        MowerAgentHelper.SetAction("Acceleration", act, 1f);
	    MowerAgentHelper.SetAction("Rotation", act, Random.Range(-1f, 1f));
        return act;
    }

    public float[] MakeMemory(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
    {
        return new float[0];
		
    }
}
