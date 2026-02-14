using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.RankSystem
{
    public class RankManager : MonoBehaviour
    {
        [SerializeField] private RankConfig rankConfig;
        public int CurrentRank { get; private set; }
        public float RankProgress { get; private set; }

        public event Action OnRankChanged;

        private Queue<float> pointsRequirementQueue;
        private float pointsToRankUp;
        private float currentPoints;

        private void Start()
        {
            pointsRequirementQueue = new Queue<float>();
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank1);
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank2);
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank3);
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank4);
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank5);
            pointsRequirementQueue.Enqueue(rankConfig.PointsToRank6);

            pointsToRankUp = pointsRequirementQueue.Dequeue();
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IncreasePoints(100f);
            }
        }
        public void IncreasePoints(float value)
        {   
            Debug.Log($"Adding {value} points");
            currentPoints += value;
            RankProgress = Mathf.InverseLerp(0f, pointsToRankUp, currentPoints);

            if (RankProgress >= 1f)
            {
                RankUp();
            }
        }

        private void RankUp()
        {
            if (CurrentRank == 6)
            {
                Debug.Log("Max Rank Reached");
                return;
            }
            CurrentRank++;
            if (pointsRequirementQueue.TryDequeue(out float result)) pointsToRankUp = result;
            RankProgress = Mathf.InverseLerp(0f, pointsToRankUp, currentPoints);
            OnRankChanged?.Invoke();
            Debug.Log($"Rank Up!\nCurrent Rank: {CurrentRank}\nRank Progress: {RankProgress}" +
                $"\nCurrent Points: {currentPoints}\nPoints to Rank Up: {pointsToRankUp}");
        }
    }
}

