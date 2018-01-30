using UnityEngine;

namespace Helpers
{
    public static class GizmosExtended
    {
        private static Color retainedGizmosColor;

        #region Color

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Previous Gizmos.color</returns>
        public static Color SetColor(Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            return previousColor;
        }
        
        public static void RetainColor()
        {
            retainedGizmosColor = Gizmos.color;
        }

        public static void RetainColorAndChangeTo(Color color)
        {
            GizmosExtended.RetainColor();
            Gizmos.color = color;
        }

        public static void RestoreColorToRetained()
        {
            Gizmos.color = retainedGizmosColor;
        }

        #endregion
        #region RectInt

        public static void DrawRect(RectInt rect)
        {
            // Verticals
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMin, rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax));
            // Horizontals
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin));
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMax), new Vector3(rect.xMax, rect.yMax));
        }

        public static void DrawRect(RectInt rect, bool yToZ, float thirdValue = 0f)
        {
            if (!yToZ)
            {
                DrawRect(rect);
                return;
            }
            
            // Verticals
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMin), new Vector3(rect.xMin, thirdValue, rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMax, thirdValue, rect.yMin), new Vector3(rect.xMax, thirdValue, rect.yMax));
            // Horizontals
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMin), new Vector3(rect.xMax, thirdValue, rect.yMin));
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMax), new Vector3(rect.xMax, thirdValue, rect.yMax));
        }

        public static void DrawRect(RectInt rect, Color color)
        {
            GizmosExtended.DrawRect(rect, false, color);
        }

        public static void DrawRect(RectInt rect, bool yToZ, Color color)
        {
            GizmosExtended.DrawRect(rect, yToZ, 0f, color);
        }

        public static void DrawRect(RectInt rect, bool yToZ, float thirdValue, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            GizmosExtended.DrawRect(rect, yToZ, thirdValue);
            Gizmos.color = previousColor;
        }

        #endregion
        #region Rect

        public static void DrawRect(Rect rect)
        {
            // Verticals
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMin, rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax));
            // Horizontals
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin));
            Gizmos.DrawLine(new Vector3(rect.xMin, rect.yMax), new Vector3(rect.xMax, rect.yMax));
        }

        public static void DrawRect(Rect rect, bool yToZ, float thirdValue = 0f)
        {
            if (!yToZ)
            {
                DrawRect(rect);
                return;
            }
            
            // Verticals
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMin), new Vector3(rect.xMin, thirdValue, rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMax, thirdValue, rect.yMin), new Vector3(rect.xMax, thirdValue, rect.yMax));
            // Horizontals
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMin), new Vector3(rect.xMax, thirdValue, rect.yMin));
            Gizmos.DrawLine(new Vector3(rect.xMin, thirdValue, rect.yMax), new Vector3(rect.xMax, thirdValue, rect.yMax));
        }

        public static void DrawRect(Rect rect, Color color)
        {
            GizmosExtended.DrawRect(rect, false, color);
        }

        public static void DrawRect(Rect rect, bool yToZ, Color color)
        {
            GizmosExtended.DrawRect(rect, yToZ, 0f, color);
        }

        public static void DrawRect(Rect rect, bool yToZ, float thirdValue, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            GizmosExtended.DrawRect(rect, yToZ, thirdValue);
            Gizmos.color = previousColor;
        }

        #endregion
        #region Cube

        public static void DrawCube(Vector3 center, Vector3 size)
        {
            Gizmos.DrawCube(center, size);
        }

        public static void DrawCube(Vector3 center, Vector3 size, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            Gizmos.DrawCube(center, size);
            Gizmos.color = previousColor;
        }

        public static void DrawCube(Bounds bounds)
        {
            Gizmos.DrawCube(bounds.center, bounds.size);
        }

        public static void DrawCube(Bounds bounds, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            GizmosExtended.DrawCube(bounds);
            Gizmos.color = previousColor;
        }

        public static void DrawCube(BoundsInt bounds)
        {
            Gizmos.DrawCube(bounds.center, bounds.size);
        }

        public static void DrawCube(BoundsInt bounds, Color color)
        {
            Color previousColor = Gizmos.color;
            Gizmos.color = color;
            GizmosExtended.DrawCube(bounds);
            Gizmos.color = previousColor;
        }

        #endregion
        #region Wire Cube

        public static void DrawWireCube(Vector3 center, Vector3 size)
        {
            Gizmos.DrawWireCube(center, size);
        }

        public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            Gizmos.DrawWireCube(center, size);
            Gizmos.color = previousColor;
        }

        public static void DrawWireCube(Bounds bounds)
        {
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        public static void DrawWireCube(Bounds bounds, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            GizmosExtended.DrawWireCube(bounds);
            Gizmos.color = previousColor;
        }

        public static void DrawWireCube(BoundsInt bounds)
        {
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        public static void DrawWireCube(BoundsInt bounds, Color color)
        {
            Color previousColor = GizmosExtended.SetColor(color);
            GizmosExtended.DrawWireCube(bounds);
            Gizmos.color = previousColor;
        }

        #endregion
    }
}