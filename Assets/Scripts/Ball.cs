using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

public class Ball : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void BallHit(string data);

	static Ball instance;
	SphereCollider myCollider;
	Rigidbody body;
	Vector3 moveDir;
	float movePower;
	const float initPosY = 0.8f;
	Vector3 prevPos, prevDir;
	bool isCheated;

	private Ball() { }

	void Awake()
	{
		instance = this;
		myCollider = GetComponent<SphereCollider>();
		body = GetComponent<Rigidbody>();
		movePower = 0f;
		moveDir = Vector3.zero;
		prevPos = Vector3.zero;
		prevDir = Vector3.zero;
		isCheated = false;
		Initialize();
	}

	void Update()
	{
		body.velocity = movePower * Time.deltaTime * moveDir;
		//transform.Translate(moveDir * movePower * Time.deltaTime);
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

	public void SetColliderOff()
	{
		myCollider.enabled = false;
	}

	public void ResetBall(Vector3 dir)
	{
		dir = dir.normalized;
		moveDir = dir;
		prevDir = dir;
		prevPos = Vector3.zero;
		transform.position = new Vector3(0, initPosY, 0);
	}

	public void SetBallSpeed(float speed)
	{
		movePower = speed;
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
	// y증가량 / x증가량
	/*
	 	1. 이전에 공이 부딪혀서 나온 반사각의 기울기를 구함
		2. 현재 공이 부딪힌 위치와 이전에 공이 부딪힌 위치의 기울기를 구함
		3. 이를 비교하여 같은 기울기가 나오는지 판단한다.
	 */
	const float maxX = 19.245f;
	const float minX = -19.245f;
	const float maxZ = 14.245f;
	const float minZ = -14.245f;

	const float modMaxX = 18.25f;
	const float modMinX = -18.25f;
	const float modMaxZ = 13.25f;
	const float modMinZ = -13.25f;
	// Only detect collision when you are the host (== leftPlayer)
	void OnCollisionEnter(Collision collision)
	{
		float prevIncline = prevDir.z / prevDir.x;

		Vector3 curDir = new Vector3(transform.position.x - prevPos.x, 0f, transform.position.z - prevPos.z).normalized;
		float curIncline = curDir.z / curDir.x;
		float diff = Mathf.Abs(prevIncline - curIncline);

		Debug.Log(prevIncline + ",         " + curIncline + ",         " + diff + ",       dir : " + moveDir);

		/*		Vector3 normal = collision.contacts[0].normal; // 법선벡터
				moveDir = Vector3.Reflect(moveDir, normal).normalized; // 반사벡터*/

		/*		if (collision.gameObject.tag == "Detector")
					moveDir.x = -moveDir.x;
				else if (collision.gameObject.tag == "Paddle")
				{
					Vector3 normal = collision.contacts[0].normal; // 법선벡터
					moveDir = Vector3.Reflect(moveDir, normal).normalized; // 반사벡터
				}
				else
					moveDir.z = -moveDir.z;*/
		if (collision.gameObject.CompareTag("Detector") || collision.gameObject.CompareTag("Paddle"))
			moveDir.x = -moveDir.x;
		else
			moveDir.z = -moveDir.z;

		transform.Translate(moveDir * Time.deltaTime);
/*		if (transform.position.x > maxX || transform.position.x < minX)
		{
			Debug.Log("보정 전 : " + transform.position.x + ", " + transform.position.z);
			transform.position = new Vector3(transform.position.x < 0 ? modMinX : modMaxX, transform.position.y, transform.position.z);
			Debug.Log("보정 후 : " + transform.position.x + ", " + transform.position.z);
		}
		if (transform.position.z > maxZ || transform.position.z < minZ)
		{
			Debug.Log("보정 전 : " + transform.position.x + ", " + transform.position.z);
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z < 0 ? modMinZ : modMaxZ);
			Debug.Log("보정 후 : " + transform.position.x + ", " + transform.position.z);
		}
*/
		if (float.IsNaN(diff) || float.IsInfinity(diff) || diff <= 1f)
		{
			bool isDetector;
			if (collision.gameObject.CompareTag("Detector"))
				isDetector = true;
			else
				isDetector = false;
			string data = JsonUtility.ToJson(new JsonStructs.BallHit(transform.position, moveDir, isDetector));
		}
		else
		{
			Debug.Log("Cheating!!");
			/*moveDir = Vector3.zero;*/
			Debug.Log(transform.position.x + ", " + transform.position.z + ", " + prevPos.x + ", " + prevPos.z);
		}
		prevPos = transform.position;
		prevDir = moveDir;

		// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
			BallHit(data);
#endif
		/*if (collision.gameObject.tag == "Detector")
			GameManager.GetInstance().Next();*/
	}

	// call from react
	public void SynchronizeBallPos(string ballData)
	{
		JsonStructs.BallHit bhs = JsonUtility.FromJson<JsonStructs.BallHit>(ballData);
/*		bhs.ballPosX = Mathf.Clamp(bhs.ballPosX, -19.2f, 19.2f);
		bhs.ballPosZ = Mathf.Clamp(bhs.ballPosZ, -14.2f, 14.2f);*/

		moveDir = new Vector3(bhs.ballDirX, bhs.ballDirY, bhs.ballDirZ);
		moveDir = moveDir.normalized;
		transform.position = new Vector3(bhs.ballPosX, bhs.ballPosY, bhs.ballPosZ);
	}
}
