using UnityEngine;
using GameInput;
using BuildingSystem.Models;
using Photon.Pun;
using System.Collections.Generic;


namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        [SerializeField]
        public List<BuildableItem> _buildables;

        [field:SerializeField]
        public BuildableItem ActiveBuildable;

        private int ActiveBuildableIndex;

        [SerializeField]
        private float _maxBuildingDistance = 2000f;

        [SerializeField]
        private ConstructionLayer _constructionLayer;

        [SerializeField]
        private PreviewLayer _previewLayer;

        [SerializeField]
        private MouseUser _mouseUser;

        private PhotonView m_PV;

        private void Start()
        {
            m_PV = this.GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (!IsMouseWithinBuildableRange()) _previewLayer.ClearPreview();
            if (_constructionLayer == null) return;
            var mousePos = _mouseUser.MouseInWorldPosition;
            Collider2D collider = Physics2D.OverlapPoint(mousePos);

            if (_mouseUser.IsMouseButtonPressed(MouseButton.Right))
            {
                _constructionLayer.Destroy(mousePos);
            }
            if (ActiveBuildable == null) return;
            
            var isSpaceEmpty = _constructionLayer.IsEmpty(mousePos,
                ActiveBuildable.UseCustomCollisionSpace ? ActiveBuildable.CollisionSpace : default);

            _previewLayer.ShowPreview(ActiveBuildable, mousePos, isSpaceEmpty && collider == null);
            if (_mouseUser.IsMouseButtonPressed(MouseButton.Left) && isSpaceEmpty && collider == null)
            {
                //_constructionLayer.Build(mousePos, ActiveBuildable);
                m_PV.RPC("InstallHouseObject", RpcTarget.AllBuffered, mousePos, ActiveBuildableIndex);
            }
        }

        private bool IsMouseWithinBuildableRange()
        {
            return Vector3.Distance(_mouseUser.MouseInWorldPosition, transform.position) <= _maxBuildingDistance;
        }

        public void SetActiveBuildable(int index)
        {
            ActiveBuildableIndex = index;
            ActiveBuildable = _buildables[index];
        }

        [PunRPC]
        private void InstallHouseObject(Vector2 mousePos, int index)
        {

     
            SetActiveBuildable(index);
            _constructionLayer.Build(mousePos, ActiveBuildable);
            
        }
    }
}
