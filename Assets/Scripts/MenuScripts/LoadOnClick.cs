using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {
	public void LoadScence (int level)
	{
		Application.LoadLevel(level);
	}
}
