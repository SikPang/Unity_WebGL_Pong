using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonStructs
{
	public struct StartGame
	{
		public Vector3 ballDir;
		public bool isFirst;
		public StartGame(Vector3 ballDir, bool isFirst)
		{
			this.ballDir = ballDir;
			this.isFirst = isFirst;
		}
	}

	public struct GameOver
	{
		public Enums.PlayerSide winner;
		public int leftScore;
		public int rightScore;
		public string reason;
		public GameOver(Enums.PlayerSide winner, int leftScore, int rightScore, string reason)
		{
			this.winner = winner;
			this.leftScore = leftScore;
			this.rightScore = rightScore;
			this.reason = reason;
		}
	}
}
