using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	const float leftInitPosX = -18.5f;
	const float rightInitPosX = 18.5f;
	const float initPosY = 0.85f;
	const float movePower = 1000f;

	[DllImport("__Internal")]
	private static extern void MovePaddle(string data);

	[SerializeField] Enums.PlayerSide paddleSide;
	Rigidbody body;
	float dir;
	bool isAvailable;
	GameManager gameManager;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		isAvailable = false;
		dir = 0f;
		ResetPos();
	}

	void Start()
	{
		gameManager = GameManager.GetInstance();
	}

	void Update()
	{
		if (!isAvailable)
			return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
			dir += movePower;
		if (Input.GetKeyUp(KeyCode.UpArrow))
			dir -= movePower;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			dir -= movePower;
		if (Input.GetKeyUp(KeyCode.DownArrow))
			dir += movePower;

		//body.MovePosition(body.position + moveDir * movePower * Time.deltaTime);
		body.velocity = new Vector3(0, 0, dir * Time.deltaTime);
		if (dir != 0f && !gameManager.GetIsOver())
		{
			string pos = JsonUtility.ToJson(new JsonStructs.MovePaddle(transform.position));

			// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
			MovePaddle(pos);
#endif
		}
	}

	public void ResetPos()
	{
		if (paddleSide == Enums.PlayerSide.LEFT)
			transform.position = new Vector3(leftInitPosX, initPosY, 0);
		else
			transform.position = new Vector3(rightInitPosX, initPosY, 0);
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}

	public Vector3 GetRotation()
	{
		return transform.rotation.eulerAngles;
	}

	public Vector3 GetScale()
	{
		return transform.localScale;
	}

	public float GetSpeed()
	{
		return movePower;
	}

	public void SetAvailable()
	{
		isAvailable = true;
	}

	// call from react
	public void MoveOpponentPaddle(string paddlePos)
	{
		JsonStructs.MovePaddle pos = JsonUtility.FromJson<JsonStructs.MovePaddle>(paddlePos);
		transform.position = new Vector3(pos.paddlePosX, pos.paddlePosY, pos.paddlePosZ);
	}
}
