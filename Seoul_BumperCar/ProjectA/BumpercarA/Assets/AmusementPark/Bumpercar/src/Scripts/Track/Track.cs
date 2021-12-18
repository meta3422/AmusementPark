using UnityEngine;
using System.Collections.Generic;
using PathCreation;

namespace BC.Track
{
    public class Track : MonoBehaviour
    {

        public TrackPoint origin;

        List<TrackPoint> trackPoints;
        PathCreator pathCreator;

        public float OriginDistance { get; private set; }
        public float Length { get; private set; }

        private void Awake()
        {
            if (!Application.isPlaying) return;
            
            Debug.Assert(origin, "Track : origin point must be assigned");

            if (pathCreator == null) pathCreator = GetComponent<PathCreator>();
            OriginDistance = pathCreator.path.GetClosestDistanceAlongPath(origin.transform.position);
            Length = pathCreator.path.length;

            var trackPointsArray = GetComponentsInChildren<TrackPoint>();
            trackPoints = new List<TrackPoint>();
            for(int i = 0; i < trackPointsArray.Length; i++)
                trackPoints.Add(trackPointsArray[i]);
        }

        public void Init()
        {
            InitializeTrackPoints();
            SortTrackPoints();
        }

        void InitializeTrackPoints()
        {
            for (int i = 0; i < trackPoints.Count; i++)
                trackPoints[i].Init();
        }

        void SortTrackPoints()
        {
            trackPoints.Sort(SortByOriginDistance);
        }

        int SortByOriginDistance(TrackPoint a, TrackPoint b)
        {
            if (a.relativeDistance < b.relativeDistance) return -1;
            if (a.relativeDistance < b.relativeDistance) return 1;

            return 0;
        }

        public float GetClosestDistance(Vector3 position)
        {
            return pathCreator.path.GetClosestDistanceAlongPath(position);
        }

        public Vector3 GetClosestPoint(Vector3 position)
        {
            return pathCreator.path.GetClosestPointOnPath(position);
        }

        public Vector3 GetPointAtDistance(float distance)
        {
            return pathCreator.path.GetPointAtDistance(distance);
        }

        public Quaternion GetRotationAtDistance(float distance)
        {
            return pathCreator.path.GetRotationAtDistance(distance);
        }

        public float GetRelativeDistanceToOrigin(float distance)
        {
            float res = distance - OriginDistance;
            if (res < 0) res += Length;
            return res;
        }

    }
}