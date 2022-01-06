using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public Animator doorController;
	public GameObject mainRoom;

    public void Activate()
	{
		bool open = doorController.GetBool("character_nearby");
		doorController.SetBool("character_nearby", !open);
		foreach (Transform child in transform)
		{
			foreach (Transform baby in child)
			{
				baby.gameObject.layer = 0;
			}
			child.gameObject.layer = 0;
		}
	}
}
