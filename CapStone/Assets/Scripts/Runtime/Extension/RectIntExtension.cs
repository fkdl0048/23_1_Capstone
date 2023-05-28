using UnityEngine;

namespace Extension
{
    public static class RectIntExtension
    {
        public delegate void RectAction(Vector3Int coords);

        public delegate bool RectActionBool(Vector3Int coords);
        public static void Iterate(this RectInt rect, Vector3Int coords, RectAction action)
        {
            var distance_x = rect.xMax - rect.x;
            var distance_y = rect.yMax - rect.y;
            for (var x = rect.x - distance_x / 2; x < rect.xMax - distance_x / 2; x++)
            {
                for (var y = rect.y - distance_y / 2; y < rect.yMax - distance_y / 2; y++)
                {
                    action(coords + new Vector3Int(x, y, 0));
                }
            }
        }
        
        public static bool Iterate(this RectInt rect, Vector3Int coords, RectActionBool action)
        {
            var distance_x = rect.xMax - rect.x;
            var distance_y = rect.yMax - rect.y;
            for (var x = rect.x - distance_x / 2; x < rect.xMax - distance_x / 2; x++)
            {
                for (var y = rect.y - distance_y / 2; y < rect.yMax - distance_y / 2; y++)
                {
                    if (action(coords + new Vector3Int(x, y, 0)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
    }
}