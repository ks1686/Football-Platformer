using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            var controls = col.GetComponent<CharacterControls>();
            if (controls != null)
            {
                controls.LoadCheckPoint();
            }
        }
	}
}
