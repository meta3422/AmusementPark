using UnityEngine;
using UnityEngine.SceneManagement;
using PathCreation;

namespace BC.Track
{
    public class StageSceneManager : MonoBehaviour
    {
        public Track track;
        public TrackFollower player;

        int currentStage;

        private void Awake()
        {
            if (track == null) track = FindObjectOfType<Track>();
            Debug.Assert(track, "StageSceneManager : Can't find track");

            if (player == null) player = FindObjectOfType<TrackFollower>();
            Debug.Assert(player, "StageSceneManager : Can't find player");

            currentStage = 0;
        }

        private void Start()
        {
            track.Init();
            player.PositionToOrigin();
        }

        private void Update()
        {
            
        }


    }
}