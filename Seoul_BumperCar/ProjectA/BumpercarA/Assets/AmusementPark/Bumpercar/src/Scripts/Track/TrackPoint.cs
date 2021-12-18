using PathCreation;
using UnityEngine;

namespace BC.Track
{
    public class TrackPoint : MonoBehaviour
    {
        Track track;
        public bool isOpen = false;
        public Animator flagAnimator;

        public float Distance { get; private set; }
        public float relativeDistance { get; private set; }

        void SetPositionToClosestPoint()
        {
            Distance = track.GetClosestDistance(transform.position);
            transform.position = track.GetPointAtDistance(Distance);
            relativeDistance = track.GetRelativeDistanceToOrigin(Distance);
        }

        public void Init()
        {
            track = GetComponentInParent<Track>();
            SetPositionToClosestPoint();
            if (isOpen) flagAnimator.SetTrigger("spawn");
        }

        public void OpenNewStage()
        {
            if (isOpen) return;
            flagAnimator.SetTrigger("spawn");
        }

    }
}