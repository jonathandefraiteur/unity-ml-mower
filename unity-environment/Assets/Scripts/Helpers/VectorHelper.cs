using UnityEngine;

namespace Helpers
{
    public static class VectorHelper
    {
        public static Vector3 RandomVector3(int minX, int maxX, int minY, int maxY)
        {
            return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        
        public static Vector3 RandomVector3(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }
        
        public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY)
        {
            return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        
        public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        }

        public static Vector3 RandomVector3(Rect rect, float thirdValue = 0f,  bool yToZ = false)
        {
            return yToZ ? new Vector3(Random.Range(rect.xMin, rect.xMax), thirdValue, Random.Range(rect.yMin, rect.yMax))
                        : new Vector3(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax), thirdValue);
        }

        public static Vector3 RandomVector3(RectInt rect, float thirdValue = 0f,  bool yToZ = false)
        {
            return yToZ ? new Vector3(Random.Range(rect.xMin, rect.xMax), thirdValue, Random.Range(rect.yMin, rect.yMax))
                        : new Vector3(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax), thirdValue);
        }

        public static Vector3 RandomVector3(Bounds bounds)
        {
            return RandomVector3(bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y, bounds.min.z, bounds.max.z);
        }

        public static Vector3 RandomVector3(BoundsInt bounds)
        {
            return RandomVector3(bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y, bounds.min.z, bounds.max.z);
        }
    }
}