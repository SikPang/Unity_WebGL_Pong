using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
	static Ball instance;
	Vector3 moveDir;
	const float movePower = 15f;
	const float initPosY = 0.8f;

	void Awake()
	{
		instance = this;
		Initialize();
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

	public void Reset()
	{
		float randX = Random.Range(0, 2) * -2 + 1;
		float randZ = (Random.Range(0, 2) * -2 + 1) * Random.Range(0.2f, 0.7f);
		moveDir.x = randX;
		moveDir.z = randZ;
		transform.position = new Vector3(0, initPosY, 0);
	}

	void OnCollisionEnter(Collision collision)
	{
		Vector3 normal = collision.contacts[0].normal; // ¹ý¼±º¤ÅÍ
		moveDir = Vector3.Reflect(moveDir, normal).normalized; // ¹Ý»çº¤ÅÍ
	}
}
