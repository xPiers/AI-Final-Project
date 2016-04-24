﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {

    public List<WaveEvent> events = new List<WaveEvent> ();

    private bool isPlaying = false;

    public void StartWave ()
    {
        isPlaying = true;

        if (events.Count != 0)
            events[0].StartEvent ();
        else
        {
            LevelManager.Instance.EndWave ();
        }
    }

    private void Update ()
    {
        if (!isPlaying)
            return;

        if (!events[0].RunEvent ())
        {
            Debug.Log ("End Event");
            events.RemoveAt (0);
            if (events.Count == 0)
                LevelManager.Instance.EndWave ();
            else
                events[0].StartEvent ();
        }
    }

    [System.Serializable]
    public class WaveEvent
    {
        public float duration = 15.0f;
        public List<SpawnInfo> spawnInfos;
        private float startTime;

        public void StartEvent ()
        {
            startTime = Time.time;
        }

        public bool RunEvent ()
        {
            if (duration == 0.0f && spawnInfos.Count == 0)
                return false;
            else if (Time.time - startTime > duration && duration != 0.0f)
                return false;

            for (int i = 0; i < spawnInfos.Count; i++)
            {
                spawnInfos[i].ReadyToSpawn ();

                if (spawnInfos[i].amount == 0)
                {
                    spawnInfos.RemoveAt (i);
                }
            }

            return true;
        }

        [System.Serializable]
        public class SpawnInfo
        {
            public int spawnPointIndex = 0;
            public int spawnPrefabIndex = 0;
            public int amount = 10;
            public float interval = 1.0f;

            private float lastTime;

            public void ReadyToSpawn ()
            {
                if (Time.time - lastTime >= interval)
                {
                    SpawnManager.Instance.Spawn(spawnPrefabIndex, spawnPointIndex);
                    amount--;
                    lastTime = Time.time;
                }
            }
        }
    }
}
