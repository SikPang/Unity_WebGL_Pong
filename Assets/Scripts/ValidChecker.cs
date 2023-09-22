using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ValidChecker : MonoBehaviour
{
	struct ValidCheckStruct
	{
		public float leftPosX;
		public float leftPosY;
		public float leftPosZ;
		public float leftRotateX;
		public float leftRotateY;
		public float leftRotateZ;
		public float leftScaleX;
		public float leftScaleY;
		public float leftScaleZ;
		public float leftSpeed;

		public float rightPosX;
		public float rightPosY;
		public float rightPosZ;
		public float rightRotateX;
		public float rightRotateY;
		public float rightRotateZ;
		public float rightScaleX;
		public float rightScaleY;
		public float rightScaleZ;
		public float rightSpeed;

		public float ballPosX;
		public float ballPosY;
		public float ballPosZ;
		public float ballScaleX;
		public float ballScaleY;
		public float ballScaleZ;
		public float ballSpeed;
	}

	[DllImport("__Internal")]
	private static extern void ValidCheck(string data);
	static ValidChecker instance;
	[SerializeField] Paddle leftPaddle;
	[SerializeField] Paddle rightPaddle;
	Ball ball;

	private ValidChecker() { }

	void Awake()
	{
		instance = this;
		ball = Ball.GetInstance();
	}

	public static ValidChecker GetInstance()
	{
		return instance;
	}

	ValidCheckStruct GetValidCheckStruct()
	{
		ValidCheckStruct vcs = new ValidCheckStruct();
		Vector3 leftPos = leftPaddle.GetPos();
		Vector3 leftRotate = leftPaddle.GetRotation();
		Vector3 leftScale = leftPaddle.GetScale();
		vcs.leftPosX = leftPos.x;
		vcs.leftPosY = leftPos.y;
		vcs.leftPosZ = leftPos.z;
		vcs.leftRotateX = leftRotate.x;
		vcs.leftRotateY = leftRotate.y;
		vcs.leftRotateZ = leftRotate.z;
		vcs.leftScaleX = leftScale.x;
		vcs.leftScaleY = leftScale.y;
		vcs.leftScaleZ = leftScale.z;
		vcs.leftSpeed = leftPaddle.GetSpeed();

		Vector3 rightPos = rightPaddle.GetPos();
		Vector3 rightRotate = rightPaddle.GetRotation();
		Vector3 rightScale = rightPaddle.GetScale();
		vcs.rightPosX = rightPos.x;
		vcs.rightPosY = rightPos.y;
		vcs.rightPosZ = rightPos.z;
		vcs.rightRotateX = rightRotate.x;
		vcs.rightRotateY = rightRotate.y;
		vcs.rightRotateZ = rightRotate.z;
		vcs.rightScaleX = rightScale.x;
		vcs.rightScaleY = rightScale.y;
		vcs.rightScaleZ = rightScale.z;
		vcs.rightSpeed = rightPaddle.GetSpeed();

		Vector3 ballPos = ball.GetPos();
		Vector3 ballScale = ball.GetScale();
		vcs.ballPosX = ballPos.x;
		vcs.ballPosY = ballPos.y;
		vcs.ballPosZ = ballPos.z;
		vcs.ballScaleX = ballScale.x;
		vcs.ballScaleY = ballScale.y;
		vcs.ballScaleZ = ballScale.z;
		vcs.ballSpeed = ball.GetSpeed();

		return vcs;
	}

	public IEnumerator StartValidCheck()
	{
		while (true)
		{
			float nextTime = Random.Range(10f, 60f);

			yield return new WaitForSecondsRealtime(nextTime);

			string data = JsonUtility.ToJson(GetValidCheckStruct());

#if UNITY_WEBGL == true && UNITY_EDITOR == false
	ValidCheck(data);
#endif
		}
	}
}
