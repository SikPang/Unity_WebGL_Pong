using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
	public enum GameMode
	{
		NONE = -1,
		NORMAL = 0,
		HARD = 1,
	}

	public enum GameType
	{
		NONE = -1,
		MATCH = 0,
		FRIEND = 1,
	}

	public enum PlayerSide
	{
		NONE = -1,
		LEFT = 0,
		RIGHT = 1,
	}

	public enum GameEndStatus
	{
		NONE = -1,
		NORNAL = 0,
		CHEATING = 1,
		DISCONNECT = 2,
	}

	public enum PlayerStatus
	{
		NONE = -1,
		WAITING = 0,
		PLAYING = 1,
	}
}
