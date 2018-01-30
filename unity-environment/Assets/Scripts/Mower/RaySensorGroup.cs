using System;
using System.Linq;
using UnityEngine;

namespace Mower
{
    [Serializable]
    public class RaySensorGroup
    {
        public string Label;
        public Color Color;
        public Vector3 Origin;
        public RaySensor[] RaySensors;
        public LayerMask LayerMask;
        public QueryTriggerInteraction QueryTriggerInteraction;

        #region Constructors
        
        public RaySensorGroup(string label, Color color, Vector3 origin, RaySensor[] raySensors, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            Label = label;
            Color = color;
            Origin = origin;
            RaySensors = raySensors;
            LayerMask = layerMask;
            QueryTriggerInteraction = queryTriggerInteraction;
        }
        
        #endregion
        #region Methods

        public RaySensor[] GlobalRaySensors()
        {
            var globalRaySensors = new RaySensor[RaySensors.Length];
            for (var i = 0; i < RaySensors.Length; i++)
            {
                globalRaySensors[i] = new RaySensor(RaySensors[i].Label, RaySensors[i].Origin + Origin, RaySensors[i].Direction, RaySensors[i].MaxDistance);
            }
            return globalRaySensors;
        }

        public RaySensor Get(string label)
        {
            return RaySensors.FirstOrDefault(raySensor => raySensor.Label == label);
        }
        
        #endregion
    }
}