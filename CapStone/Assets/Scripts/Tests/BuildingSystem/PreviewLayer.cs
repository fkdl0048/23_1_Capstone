using BuildingSystem.Models;
using UnityEngine;

namespace BuildingSystem
{
    public class PreviewLayer : TilemapLayer
    {
        [SerializeField]
        private SpriteRenderer _previewRederer;

        public void ShowPreview(BuildableItem item, Vector3 worldCoords, bool isValid)
        {
            var coords = _tilemap.WorldToCell(worldCoords);
            _previewRederer.enabled = true;
            _previewRederer.transform.position = _tilemap.CellToWorld(coords) + _tilemap.cellSize / 2;
            _previewRederer.sprite = item.PreviewSprite;
            _previewRederer.color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        }

        public void ClearPreview()
        {
            _previewRederer.enabled = false;
        }
    }
}
