using UnityEngine;

namespace Mower
{
    public class RaySensorHit
    {
        public readonly string Label;
        public readonly RaycastHit[] Hits;

        public RaySensorHit(string label, RaycastHit[] hits)
        {
            Label = label;
            this.Hits = hits;
        }
    }
}