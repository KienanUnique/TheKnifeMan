using Game.Utils.Directions;
using UnityEngine;

namespace Game.Utils
{
    public static class Vector2Extension
    {
        public static EDirection2D ToDirection2D(this Vector2 vector)
        {
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
                return vector.x > 0 ? EDirection2D.Right : EDirection2D.Left;

            return vector.y > 0 ? EDirection2D.Up : EDirection2D.Down;
        }
        
        public static EDirection1D ToDirection1D(this Vector2 vector)
        {
            return vector.x > 0 ? EDirection1D.Right : EDirection1D.Left;
        }
    }
}