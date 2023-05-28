using UnityEngine;
using GameInput;
using BuildingSystem.Models;
using Photon.Pun;

namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        [field:SerializeField]
        public BuildableItem ActiveBuildable { get; private set; }

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
                m_PV.RPC("InstallHouseObject", RpcTarget.AllBuffered, mousePos /*, index*/);
            }
        }

        private bool IsMouseWithinBuildableRange()
        {
            return Vector3.Distance(_mouseUser.MouseInWorldPosition, transform.position) <= _maxBuildingDistance;
        }

        public void SetActiveBuildable(BuildableItem item)
        {
            ActiveBuildable = item;
        }

        [PunRPC]
        private void InstallHouseObject(Vector2 mousePos, int index)
        {
            if(m_PV.IsMine)
            {
                //index¸¦ ³Ñ°ÜÁà¾ßµÊ.
                _constructionLayer.Build(mousePos, ActiveBuildable);
            }
        }
    }
}
