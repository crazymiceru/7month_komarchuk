using UnityEngine;

namespace MobileGame
{

    public sealed class UnitViewTraectory : MonoBehaviour, ITraectory
    {
        private Color _clr = Color.red;
        private float _sizeTrack = 0.05f;

        [SerializeField] private Traectory[] _track;
        public Traectory[] Track => _track;

        private void OnDrawGizmos()
        {
            if (_track == null || _track.Length <= 1) return;

            Gizmos.color = _clr;            
            Vector3 pos;
            Quaternion rotate = new Quaternion();

            for (int i = 1; i < _track.Length; i++)
            {
                if (!trackIsRelevant(i)) continue;
                pos = (_track[i - 1].transform.position + _track[i].transform.position) / 2;
                var size = new Vector3(_sizeTrack, _sizeTrack, Vector3.Distance(_track[i - 1].transform.position, _track[i].transform.position));
                rotate.SetLookRotation(_track[i].transform.position - _track[i - 1].transform.position, Vector3.up);
                Gizmos.matrix = Matrix4x4.TRS(pos, rotate, Vector3.one);
                Gizmos.DrawCube(Vector3.zero, size);
            }

            bool trackIsRelevant(int i)
            {
                return  _track[i - 1].transform != null 
                        && _track[i].transform != null 
                        && _track[i - 1].transform != _track[i].transform;
            }
        }
    }
}