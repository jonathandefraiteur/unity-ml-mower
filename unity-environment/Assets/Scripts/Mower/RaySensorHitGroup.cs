using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mower
{
    public class RaySensorHitGroup
    {
        public string Label;
        public List<RaySensorHit> RaySensorHits;

        public RaySensorHitGroup(string label)
        {
            Label = label;
            RaySensorHits = new List<RaySensorHit>();
        }
        
        public RaySensorHitGroup(string label, IEnumerable<RaySensorHit> raySensorHits)
        {
            Label = label;
            RaySensorHits = new List<RaySensorHit>(raySensorHits);
        }
    }
}