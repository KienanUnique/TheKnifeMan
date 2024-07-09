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
    }
}