using System.Collections.Generic;
using UnityEngine;

namespace Mower
{
    public class MowerBlade : MonoBehaviour
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask mask;

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(position), radius);
        }

        public ClodOfGrass[] MowGrass()
        {
            var mownClodOfGrass = new List<ClodOfGrass>();
            
            Collider[] colliders = Physics.OverlapSphere (transform.TransformPoint(position), radius, mask.value);
            foreach (Collider col in colliders)
            {
                var grassScript = col.GetComponent<ClodOfGrass>();
                if (grassScript == null || grassScript.Mowned == true)
                    continue;

                // Cut
                grassScript.Mowned = true;
                mownClodOfGrass.Add(grassScript);
            }

            return mownClodOfGrass.ToArray();
        }
    }
}