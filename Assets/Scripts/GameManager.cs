using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public struct GameOverStruct
	{
		public Enums.PlayerSide winner;
		public int leftScore;
		public int rightScore;
		public string reason;
		public GameOverStruct(Enums.PlayerSide winner, int leftScore, int rightScore, string reason)
		{
			this.winner = winner;
			this.leftScore = leftScore;
			this.rightScore = rightScore;
			this.reason = reason;
		}
	}

	[DllImport("__Internal")]
	private static extern void UnityException(string reason);

	static GameManager instance;
	[SerializeField] Paddle leftPaddle;
	[SerializeField] Paddle rightPaddle;
	Score score;
	Ball ball;
	Enums.PlayerSide mySide;
	Coroutine validCheckCoroutine;

	private GameManager() { }

	void Awake()
	{
		Inintialize();
	}

	void Start()
	{
		score = Score.GetInstance();
		ball = Ball.GetInstance();
	}

	void Inintialize()
	{
		instance = this;
		mySide = Enums.PlayerSide.NONE;
	}

	public static GameManager GetInstance()
	{
		return instance;
	}

	public Enums.PlayerSide GetMySide()
	{
		return mySide;
	}

	public void NextGame(Vector3 ballDir)
	{
		ball.ReSetBall(ballDir);
		leftPaddle.Initialize();
		rightPaddle.Initialize();
	}

	// call from react
	public void SetMySide(Enums.PlayerSide side)
	{
		score.Initialize();
		mySide = side;

		if (side == Enums.PlayerSide.LEFT)
			leftPaddle.SetAvailable();
		else if (side == Enums.PlayerSide.RIGHT)
			rightPaddle.SetAvailable();
		else
		{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	UnityException("GameManager.SetMySide() : PlayerSide is NONE");
#endif
		}
	}

	// call from react
	public void StartGame(string ballDir, bool isFirst)
	{
		if (isFirst)
		{
			score.Initialize();
			validCheckCoroutine = StartCoroutine(ValidChecker.GetInstance().StartValidCheck());
		}
		Vector3 dir = JsonUtility.FromJson<Vector3>(ballDir);
		NextGame(dir);
	}

	// call from react
	public void GameOver(string data)
	{
		ball.Initialize();

		StopCoroutine(validCheckCoroutine);
		GameOverStruct gos = JsonUtility.FromJson<GameOverStruct>(data);
		score.Finish(gos);
	}

	private void Update()
	{
		// ---- Test ----
		if (Input.GetKeyDown(KeyCode.Alpha1))
			SetMySide(Enums.PlayerSide.LEFT);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			StartGame(JsonUtility.ToJson(new Vector3(1f, 0f, 1f)), true);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			StartGame(JsonUtility.ToJson(new Vector3(1f, 0f, 1f)), false);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			GameOver(JsonUtility.ToJson(new GameOverStruct(Enums.PlayerSide.LEFT, 5, 0, "Test")));
	}
}
