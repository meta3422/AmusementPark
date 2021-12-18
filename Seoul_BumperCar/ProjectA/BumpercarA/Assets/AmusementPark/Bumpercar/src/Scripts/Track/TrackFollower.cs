using PathCreation;
using UnityEngine;

namespace BC.Track
{
    public class TrackFollower : MonoBehaviour
    {
        Track track;

        float distance;
        float relDistance;

        public float speed;
        bool onMove;
        TrackPoint dest;

        private void Awake()
        {
            if (track == null) track = FindObjectOfType<Track>();
        }

        private void Update()
        {
            if (track == null) return;

            if (onMove) MoveToPosition(dest, false);

            relDistance = track.GetRelativeDistanceToOrigin(relDistance);
            transform.position = track.GetPointAtDistance(distance);
            transform.rotation = track.GetRotationAtDistance(distance);

        }

        private void MoveToPosition(TrackPoint point, bool ccw = true)
        {
            if (point == null)
            {
                onMove = true;
                return;
            }

            if(ccw)
            {
                float dest = distance > point.Distance ? point.Distance + track.Length : point.Distance;
                distance = Mathf.Lerp(distance, dest, speed * Time.deltaTime);
                if (Mathf.Abs(distance - dest) < 1.0e-2f) onMove = false;
                distance = distance % track.Length;
            }
            else
            {
                distance = distance < point.Distance ? distance + track.Length : distance;
                distance = Mathf.Lerp(distance, point.Distance, speed * Time.deltaTime);
                if (Mathf.Abs(distance - point.Distance) < 1.0e-2f) onMove = false;
                distance = distance % track.Length;
            }
        }

        public void SetDestination(TrackPoint dest)
        {
            onMove = true;
            this.dest = dest;
        }

        public void PositionToOrigin()
        {
            if (track == null) return;

            distance = track.origin.Distance;
            relDistance = track.GetRelativeDistanceToOrigin(distance);
            transform.position = track.GetPointAtDistance(distance);

            onMove = false;
        }
    }
}