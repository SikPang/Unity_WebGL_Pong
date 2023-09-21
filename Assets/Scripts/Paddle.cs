using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	public enum Side
	{
		Left, Right
	}
	[SerializeField] Side mySide;
	Rigidbody body;
	float dir = 0f;

	const float leftInitPosX = -18.5f;
	const float rightInitPosX = 18.5f;
	const float initPosY = 0.78f;
	const float movePower = 1000f;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		Initialize();
	}

	void Update()
	{
		if (mySide == Side.Left)
		{
			if (Input.GetKeyDown(KeyCode.W))
				dir += movePower;
			else if (Input.GetKeyDown(KeyCode.S))
				dir -= movePower;
			else if (Input.GetKeyUp(KeyCode.W))
				dir -= movePower;
			else if (Input.GetKeyUp(KeyCode.S))
				dir += movePower;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
				dir += movePower;
			else if (Input.GetKeyDown(KeyCode.DownArrow))
				dir -= movePower;
			else if (Input.GetKeyUp(KeyCode.UpArrow))
				dir -= movePower;
			else if (Input.GetKeyUp(KeyCode.DownArrow))
				dir += movePower;
		}
		//body.MovePosition(body.position + moveDir * movePower * Time.deltaTime);
		body.velocity = new Vector3(0, 0, dir * Time.deltaTime);
	}

	public void Initialize()
	{
		if (mySide == Side.Left)
			transform.position = new Vector3(leftInitPosX, initPosY, 0);
		else
			transform.position = new Vector3(rightInitPosX, initPosY, 0);
	}
}
