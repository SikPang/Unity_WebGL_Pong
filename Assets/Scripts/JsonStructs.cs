using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonStructs
{
	public struct StartGame
	{
		public Enums.PlayerSide side;
		public float ballDirX;
		public float ballDirY;
		public float ballDirZ;
		public int leftScore;
		public int rightScore;
		public float ballSpeed;
		public bool isFirst;
		public StartGame(Enums.PlayerSide side, float ballDirX, float ballDirY, float ballDirZ, int leftScore, int rightScore, float ballSpeed, bool isFirst)
		{
			this.side = side;
			this.ballDirX = ballDirX;
			this.ballDirY = ballDirY;
			this.ballDirZ = ballDirZ;
			this.leftScore = leftScore;
			this.rightScore = rightScore;
			this.ballSpeed = ballSpeed;
			this.isFirst = isFirst;
		}
	}

	public struct GameOver
	{
		public Enums.PlayerSide winner;
		public int leftScore;
		public int rightScore;
		public Enums.GameEndStatus reason;
		public GameOver(Enums.PlayerSide winner, int leftScore, int rightScore, Enums.GameEndStatus reason)
		{
			this.winner = winner;
			this.leftScore = leftScore;
			this.rightScore = rightScore;
			this.reason = reason;
		}
	}

	public struct BallHit
	{
		public float ballPosX;
		public float ballPosY;
		public float ballPosZ;
		public float ballDirX;
		public float ballDirY;
		public float ballDirZ;
		public BallHit(Vector3 ballPos, Vector3 ballDir)
		{
			ballPosX = ballPos.x;
			ballPosY = ballPos.y;
			ballPosZ = ballPos.z;
			ballDirX = ballDir.x;
			ballDirY = ballDir.y;
			ballDirZ = ballDir.z;
		}
	}

	public struct MovePaddle
	{
		public float paddlePosX;
		public float paddlePosY;
		public float paddlePosZ;
		public MovePaddle(float paddlePosX, float paddlePosY, float paddlePosZ)
		{
			this.paddlePosX = paddlePosX;
			this.paddlePosY = paddlePosY;
			this.paddlePosZ = paddlePosZ;
		}
		public MovePaddle(Vector3 paddlePos)
		{
			this.paddlePosX = paddlePos.x;
			this.paddlePosY = paddlePos.y;
			this.paddlePosZ = paddlePos.z;
		}
	}

	public struct ValidCheckStruct
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
		public ValidCheckStruct(Paddle leftPaddle, Paddle rightPaddle, Ball ball)
		{
			Vector3 leftPos = leftPaddle.GetPos();
			Vector3 leftRotate = leftPaddle.GetRotation();
			Vector3 leftScale = leftPaddle.GetScale();
			leftPosX = leftPos.x;
			leftPosY = leftPos.y;
			leftPosZ = leftPos.z;
			leftRotateX = leftRotate.x;
			leftRotateY = leftRotate.y;
			leftRotateZ = leftRotate.z;
			leftScaleX = leftScale.x;
			leftScaleY = leftScale.y;
			leftScaleZ = leftScale.z;
			leftSpeed = leftPaddle.GetSpeed();

			Vector3 rightPos = rightPaddle.GetPos();
			Vector3 rightRotate = rightPaddle.GetRotation();
			Vector3 rightScale = rightPaddle.GetScale();
			rightPosX = rightPos.x;
			rightPosY = rightPos.y;
			rightPosZ = rightPos.z;
			rightRotateX = rightRotate.x;
			rightRotateY = rightRotate.y;
			rightRotateZ = rightRotate.z;
			rightScaleX = rightScale.x;
			rightScaleY = rightScale.y;
			rightScaleZ = rightScale.z;
			rightSpeed = rightPaddle.GetSpeed();

			Vector3 ballPos = ball.GetPos();
			Vector3 ballScale = ball.GetScale();
			ballPosX = ballPos.x;
			ballPosY = ballPos.y;
			ballPosZ = ballPos.z;
			ballScaleX = ballScale.x;
			ballScaleY = ballScale.y;
			ballScaleZ = ballScale.z;
			ballSpeed = ball.GetSpeed();
		}
	}
}
