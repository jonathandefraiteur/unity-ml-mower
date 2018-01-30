using System;
using System.Collections.Generic;
using System.Linq;

namespace Mower.Scripts
{
    public static class MowerAgentHelper
    {
        public const int stateCount = 8 * 3;
        public const int actionCount = 2;
        
        #region State

        public static List<float> CreateState()
        {
            return new List<float>(new float[stateCount]);
        }
        
        public static float GetState(string label, List<float> state)
        {
            if (state.Count() != stateCount)
                throw new Exception("MowerAgent states are by " + stateCount + ", there is " + state.Count + " states actually.");

            return state[StateIndexTo(label)];
        }

        public static void SetState(string label, List<float> state, float value)
        {
            if (state.Count() != stateCount)
                throw new Exception("MowerAgent states are by " + stateCount + ", there is " + state.Count + " states actually.");

            state[StateIndexTo(label)] = value;
        }

        public static int StateIndexTo(string label)
        {
            switch (label)
            {
                case "LongGrass_Left_Distance":
                    return 0;
                case "LongGrass_LeftForward_Distance":
                    return 1;
                case "LongGrass_Forward_Distance":
                    return 2;
                case "LongGrass_RightForward_Distance":
                    return 3;
                case "LongGrass_Right_Distance":
                    return 4;
                case "LongGrass_RightBackward_Distance":
                    return 5;
                case "LongGrass_Backward_Distance":
                    return 6;
                case "LongGrass_LeftBackward_Distance":
                    return 7;
                    
                case "LongGrass_Left_Count":
                    return 8;
                case "LongGrass_LeftForward_Count":
                    return 9;
                case "LongGrass_Forward_Count":
                    return 10;
                case "LongGrass_RightForward_Count":
                    return 11;
                case "LongGrass_Right_Count":
                    return 12;
                case "LongGrass_RightBackward_Count":
                    return 13;
                case "LongGrass_Backward_Count":
                    return 14;
                case "LongGrass_LeftBackward_Count":
                    return 15;
                    
                case "Obstacle_Left_Distance":
                    return 16;
                case "Obstacle_LeftForward_Distance":
                    return 17;
                case "Obstacle_Forward_Distance":
                    return 18;
                case "Obstacle_RightForward_Distance":
                    return 19;
                case "Obstacle_Right_Distance":
                    return 20;
                case "Obstacle_RightBackward_Distance":
                    return 21;
                case "Obstacle_Backward_Distance":
                    return 22;
                case "Obstacle_LeftBackward_Distance":
                    return 23;
                    
                default:
                    throw new Exception("State label \"" + label + "\" doesn't exist");
            }
        }

        #endregion

        #region Action

        public static float[] CreateAction()
        {
            return new float[actionCount];
        }
        
        public static float GetAction(string label, float[] action)
        {
            if (action.Length != actionCount)
                throw new Exception("MowerAgent states are by " + actionCount + ", there is " + action.Length + " states actually.");

            return action[ActionIndexTo(label)];
        }

        public static void SetAction(string label, float[] action, float value)
        {
            if (action.Length != actionCount)
                throw new Exception("MowerAgent states are by " + actionCount + ", there is " + action.Length + " states actually.");

            action[ActionIndexTo(label)] = value;
        }

        public static int ActionIndexTo(string label)
        {
            switch (label)
            {
                case "Acceleration":
                    return 0;
                case "Rotation":
                    return 1;
                default:
                    throw new Exception("Action label \"" + label + "\" doesn't exist");
            }
        }

        #endregion
        
    }
}