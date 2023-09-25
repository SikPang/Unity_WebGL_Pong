using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void UnityException(string data);

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
	public void SetMySide(string data)
	{
		Enums.PlayerSide side = JsonUtility.FromJson<Enums.PlayerSide>(data);
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
	public void StartGame(string data)
	{
		JsonStructs.StartGame sgs = JsonUtility.FromJson<JsonStructs.StartGame>(data);
		if (sgs.isFirst)
		{
			score.Initialize();
			validCheckCoroutine = StartCoroutine(ValidChecker.GetInstance().StartValidCheck());
		}
		NextGame(sgs.ballDir);
	}

	// call from react
	public void GameOver(string data)
	{
		ball.Initialize();
		StopCoroutine(validCheckCoroutine);
		JsonStructs.GameOver gos = JsonUtility.FromJson<JsonStructs.GameOver>(data);
		score.Finish(gos);
	}

	private void Update()
	{
		// ---- Test ----
/*		if (Input.GetKeyDown(KeyCode.Alpha1))
			SetMySide(Enums.PlayerSide.LEFT);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			StartGame(JsonUtility.ToJson(new JsonUtility.Start)  JsonUtility.ToJson(new Vector3(1f, 0f, 1f)), true);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			StartGame(JsonUtility.ToJson(new Vector3(1f, 0f, 1f)), false);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			GameOver(JsonUtility.ToJson(new JsonStructs.GameOver(Enums.PlayerSide.LEFT, 5, 0, "Test")));*/
	}
}
