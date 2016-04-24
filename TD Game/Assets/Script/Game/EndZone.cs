using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {

    private void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Enemy")
        {
            LevelManager.Instance.EnemyCrossed ();
            Destroy (col.gameObject);
        }
    }
}
