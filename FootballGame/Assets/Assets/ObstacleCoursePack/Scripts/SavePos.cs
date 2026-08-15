using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	public Transform checkPoint;

	void OnTriggerEnter(Collider col)
	{
        if (col.CompareTag("Player"))
        {
            var controls = col.GetComponent<CharacterControls>();
            if (controls != null && checkPoint != null)
            {
                controls.checkPoint = checkPoint.position;
            }
        }
	}
}
