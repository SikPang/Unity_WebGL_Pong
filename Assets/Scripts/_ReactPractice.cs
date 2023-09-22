using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class move : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void IsOver(bool isOver);
	bool isOver = false;
	bool isStarted = false;

	// Start is called before the first frame update
	void Start()
	{
		
    }

	// Update is called once per frame
	void Update()
	{
		if (!isStarted)
			return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
			transform.Translate(new Vector3(0, 1f, 0));
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.Translate(new Vector3(0, -1f, 0));
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.Translate(new Vector3(1, 0, 0));
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Translate(new Vector3(-1f, 0, 0));

		if (transform.position.y >= 6 && isOver == false)
		{
			isOver = true;
			UnityCall();
		}
    }

	public void UnityCall()
	{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	IsOver(isOver);
#endif
	}

	public void Init()
	{
		transform.position = new Vector3(0, 0, 0);
	}

	public void startGame()
	{
		isStarted = true;
	}
}
