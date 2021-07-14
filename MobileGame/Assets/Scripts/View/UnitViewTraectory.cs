using UnityEngine;

namespace MobileGame
{

    public sealed class UnitViewTraectory : MonoBehaviour, ITraectory
    {
        [SerializeField] private Traectory[] _track;
        public OnTriggerView onTriggerView => _onTriggerView;
        [SerializeField] private OnTriggerView _onTriggerView;
        private Color _clr = Color.red;
        private float _sizeTrack = 0.05f;

        public Traectory[] Track
        {
            get => _track;
        }

        private void OnDrawGizmosSelected()
        {
            if (onTriggerView!=null)
            {
                var colliders = onTriggerView.GetComponents<BoxCollider2D>();
                Gizmos.color = Color.red;
                for (int i = 0; i < colliders.Length; i++)
                {
                    Gizmos.DrawWireCube(colliders[i].transform.position, colliders[i].size);
                }
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _clr;
            
            Vector3 pos;
            Quaternion rotate = new Quaternion();

            if (_track!=null && _track.Length > 1)
            {
                for (int i = 1; i < _track.Length; i++)
                {
                    if (_track[i - 1].transform != null && _track[i].transform != null && _track[i - 1].transform != _track[i].transform)
                    {
                    pos = (_track[i - 1].transform.position + _track[i].transform.position) / 2;
                    var size = new Vector3(_sizeTrack, _sizeTrack, Vector3.Distance(_track[i - 1].transform.position, _track[i].transform.position));
                    rotate.SetLookRotation(_track[i].transform.position - _track[i - 1].transform.position, Vector3.up);
                    Gizmos.matrix = Matrix4x4.TRS(pos, rotate, Vector3.one);
                    Gizmos.DrawCube(Vector3.zero, size);
                    }
                }
            }
        }
    }
}