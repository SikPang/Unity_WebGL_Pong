using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Ball : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void BallHit(string data);

	static Ball instance;
	Vector3 moveDir;
	GameManager gameManager;
	const float movePower = 15f;
	const float initPosY = 0.8f;

	private Ball() { }

	void Awake()
	{
		instance = this;
		Initialize();
	}

	void Start()
	{
		gameManager = GameManager.GetInstance();
	}

	void Update()
	{
		if (moveDir != Vector3.zero)
			transform.Translate(moveDir * movePower * Time.deltaTime);
	}

	public static Ball GetInstance()
	{
		return instance;
	}

	public void Initialize()
	{
		moveDir = Vector3.zero;
		transform.position = new Vector3(0, initPosY, 0);
	}

	public void SetBall(Vector3 dir, Vector3 pos)
	{
		moveDir = dir;
		transform.position = pos;
	}

	public void ReSetBall(Vector3 dir)
	{
		moveDir = dir;
		transform.position = new Vector3(0, initPosY, 0);
	}

	public Vector3 GetPos()
	{
		return transform.position;
	}

	public Vector3 GetScale()
	{
		return transform.localScale;
	}

	public float GetSpeed()
	{
		return movePower;
	}

	void OnCollisionEnter(Collision collision)
	{
		Vector3 normal = collision.contacts[0].normal; // ¹ý¼±º¤ÅÍ
		moveDir = Vector3.Reflect(moveDir, normal).normalized; // ¹Ý»çº¤ÅÍ

		string data = JsonUtility.ToJson(new JsonStructs.BallHit(transform.position, moveDir));

		// call js function
		if (gameManager.GetMySide() == Enums.PlayerSide.LEFT)
		{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
		BallHit(data);
#endif
		}
	}

	// call from react
	public void SynchronizeBallPos(string ballData)
	{
		JsonStructs.BallHit bhs = JsonUtility.FromJson<JsonStructs.BallHit>(ballData);
		transform.position = new Vector3(bhs.ballPosX, bhs.ballPosY, bhs.ballPosZ);
		moveDir = new Vector3(bhs.ballDirX, bhs.ballDirY, bhs.ballDirZ);
	}
}
