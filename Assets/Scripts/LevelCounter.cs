using UnityEngine;
using System.Collections;

public class LevelCounter : MonoBehaviour 
{
	private static LevelCounter instance = null;

	//Number of times levels have been loaded
	public int LevelLoadTimes = 0;

	// Use this for initialization
	void Start () 
	{
		//If instance already exists then destroy this
		if(instance){DestroyImmediate(this);return;}

		//Assign instance
		instance = this;

		//Don't destroy
		DontDestroyOnLoad(gameObject);
	}
}
