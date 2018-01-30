using System;
using UnityEngine;

namespace Mower
{
    [Serializable]
    public class RaySensor
    {
        [SerializeField] private string label;
        [SerializeField] private Vector3 origin;
        [SerializeField] private Vector3 direction;
        [SerializeField] private float maxDistance;

        #region Constructors

        public RaySensor(string label)
        {
            this.label = label;
            this.origin = Vector3.zero;
            this.direction = Vector3.zero;
            this.maxDistance = float.PositiveInfinity;
        }
        
        public RaySensor(string label, Vector3 origin, Vector3 direction, float maxDistance = float.PositiveInfinity)
        {
            this.label = label;
            this.origin = origin;
            this.direction = direction.normalized;
            this.maxDistance = maxDistance;
        }

        #endregion
        #region Properties

        public string Label
        {
            get { return this.label; }
            set { this.label = value; }
        }

        public Vector3 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public Vector3 Direction
        {
            get { return this.direction; }
            set { this.direction = value.normalized; }
        }

        public float MaxDistance
        {
            get { return this.maxDistance; }
            set { this.maxDistance = value > 0 ? value : 0; }
        }

        #endregion
        #region Methods
        
        public Vector3 GetPoint(float distance)
        {
            return this.origin + this.direction * distance;
        }

        public Vector3 GetEndPoint()
        {
            return GetPoint(maxDistance);
        }

        public RaySensor ToWorldSpace(Transform local)
        {
            return new RaySensor(label, local.TransformPoint(origin), local.TransformDirection(direction), maxDistance);
        }

        public Ray ToRay()
        {
            return new Ray(origin, direction);
        }

        #endregion
    }
}