using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoSingleton<LevelManager> {

    private int healthPoint = 10;

    private bool spawnActive = false;
    private bool waveActive = false;

    private List<Wave> waves = new List<Wave> ();

    public override void Init ()
    {
        foreach (Wave w in GetComponents<Wave> ())
            waves.Add (w);
    }

    private void Update ()
    {
        if (!waveActive)
        {

            if (Input.GetKeyDown (KeyCode.K))
                StartWave ();
        }
        else
        {
            if (!spawnActive && !GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log ("Wave cleared");
                waveActive = false;
                if (waves.Count == 0)
                    Victory ();
            }
        }
    }

    private void StartWave ()
    {
        Debug.Log ("Wave is starting");
        waves[0].StartWave ();
        spawnActive = true;
        waveActive = true;
    }

    public void EndWave ()
    {
        Debug.Log ("Wave is ending");
        Destroy (waves[0]);
        waves.RemoveAt (0);
        spawnActive = false;
    }

    public void EnemyCrossed ()
    {
        healthPoint--;

        if (healthPoint == 0)
            Defeat ();
    }

    private void Victory ()
    {
        Debug.Log ("Level is cleared");
    }

    private void Defeat ()
    {
        // Clear enemies, clean level
        Debug.Log ("Defeat");
    }
}
