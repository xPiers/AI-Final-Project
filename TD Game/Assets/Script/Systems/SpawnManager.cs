﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPoint
{
    public Transform self;
    public Transform destination;
}

public class SpawnManager : MonoSingleton<SpawnManager> {

    public List<SpawnPoint> spawnPoint = new List<SpawnPoint> ();
    public List<GameObject> spawnPrefabs = new List<GameObject> ();

    public void Spawn (int spawnPrefabIndex)
    {
        Spawn (spawnPrefabIndex, 0);
    }

    public void Spawn (int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject go = Instantiate (spawnPrefabs[ spawnPrefabIndex ], spawnPoint[ spawnPointIndex ].self.position, spawnPoint[ spawnPointIndex ].self.rotation) as GameObject;

        go.SendMessage ("SetDestination", spawnPoint[spawnPointIndex].destination);
    }

    // Debug Test
    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Alpha1))
            Spawn (0);
        if (Input.GetKeyDown (KeyCode.Alpha2))
            Spawn (1);
        if (Input.GetKeyDown (KeyCode.Alpha3))
            Spawn (0, 1);
        if (Input.GetKeyDown (KeyCode.Alpha4))
            Spawn (1, 1);
        if (Input.GetKeyDown (KeyCode.Alpha5))
            Spawn (0, 2);
        if (Input.GetKeyDown (KeyCode.Alpha6))
            Spawn (1, 2);
    }
}
