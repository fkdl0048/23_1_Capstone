using System;
using Extension;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace BuildingSystem.Models
{
    [Serializable]
    public class Buildable
    {
        [field: SerializeField]
        public Tilemap ParentTilemap { get; private set; }

        [field: SerializeField]
        public BuildableItem BuildableType { get; private set; }

        [field: SerializeField]
        public GameObject GameObject { get; private set; }

        [field: SerializeField]
        public Vector3Int Coordinates { get; private set; }

        public Buildable(BuildableItem type, Vector3Int coords, Tilemap tilemap, GameObject gameObject = null)
        {
            ParentTilemap = tilemap;
            BuildableType = type;
            Coordinates = coords;
            GameObject = gameObject;
        }

        public void Destroy()
        {
            if (GameObject != null)
            {
                Object.Destroy(GameObject);
            }
            ParentTilemap.SetTile(Coordinates, null);
        }

        public void IterateCollisionSpace(RectIntExtension.RectAction action)
        {
            BuildableType.CollisionSpace.Iterate(Coordinates, action);
        }
        
        public bool IterateCollisionSpace(RectIntExtension.RectActionBool action)
        {
            return BuildableType.CollisionSpace.Iterate(Coordinates, action);
        }
    }
}
