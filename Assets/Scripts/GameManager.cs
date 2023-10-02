using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

// # SynchronizeBallPos 보완 (max값 보정 후 테스트) -> host일 때만 공의 충돌 감지
// # startGame Delay (카운트 하기)
// # 프레임 제어
// # gameOver reason (enum to string)
// 난이도 차이 주기 (공 속도.. 이건 걍 서버에서? startGame에서 공 속도 받아와서 적용하기 구현?)
// startGame에 leftScore, rightScore 받아서 적용
// paddle 움직임 가끔 맛탱이 감..

public class GameManager : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void UnityException(string data);
	[DllImport("__Internal")]
	private static extern void ValidCheck(string data);
	[DllImport("__Internal")]
	private static extern void Init();

	static GameManager instance;
	[SerializeField] Paddle leftPaddle;
	[SerializeField] Paddle rightPaddle;
	Score score;
	Ball ball;
	Enums.PlayerSide mySide;
	Coroutine validCheckCoroutine;
	bool isOver;

	private GameManager() { }

	void Awake()
	{
		Inintialize();
	}

	void Start()
	{
		score = Score.GetInstance();
		ball = Ball.GetInstance();

		// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
		Init();
#endif
	}

	void Inintialize()
	{
		Application.targetFrameRate = 45;
		instance = this;
		isOver = false;
		mySide = Enums.PlayerSide.NONE;
	}

	public static GameManager GetInstance()
	{
		return instance;
	}

	public bool GetIsOver()
	{
		return isOver;
	}

	public Enums.PlayerSide GetMySide()
	{
		return mySide;
	}

	IEnumerator NextGame(Vector3 ballDir)
	{
		score.SetText("Get Ready...");
		score.SetTextActive(true);
		yield return new WaitForSecondsRealtime(1f);
		score.SetText("Start!");
		yield return new WaitForSecondsRealtime(1f);
		score.SetTextActive(false);

		ball.ResetBall(ballDir);
		leftPaddle.ResetPos();
		rightPaddle.ResetPos();
	}

	IEnumerator StartValidCheck()
	{
		while (true)
		{
			// 테스트용 시간
			float nextTime = Random.Range(1f, 5f);

			yield return new WaitForSecondsRealtime(nextTime);

			string data = JsonUtility.ToJson(new JsonStructs.ValidCheckStruct(leftPaddle, rightPaddle, ball));

			// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
			ValidCheck(data);
#endif
		}
	}

	// call from react
	public void StartGame(string data)
	{
		JsonStructs.StartGame sgs = JsonUtility.FromJson<JsonStructs.StartGame>(data);
		if (sgs.isFirst)
		{
			mySide = sgs.side;
			if (mySide == Enums.PlayerSide.LEFT)
				leftPaddle.SetAvailable();
			else if (mySide == Enums.PlayerSide.RIGHT)
			{
				rightPaddle.SetAvailable();
				ball.SetColliderOff();
			}
			else
			{
				// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
				UnityException("GameManager.StartGame() : PlayerSide is NONE");
#endif
			}
			score.Initialize();
			validCheckCoroutine = StartCoroutine(StartValidCheck());
		}
		StartCoroutine(NextGame(new Vector3(sgs.ballDirX, sgs.ballDirY, sgs.ballDirZ)));
		isOver = false;
	}

	// call from react
	public void GameOver(string data)
	{
		ball.Initialize();
		StopCoroutine(validCheckCoroutine);
		JsonStructs.GameOver gos = JsonUtility.FromJson<JsonStructs.GameOver>(data);
		score.Finish(gos);
		isOver = true;
	}

	private void Update()
	{
		// ---- Test ----
		if (Input.GetKeyDown(KeyCode.Alpha1))
			StartGame(JsonUtility.ToJson(new JsonStructs.StartGame(Enums.PlayerSide.LEFT, 1f, 0f, 1f, true)));
		if (Input.GetKeyDown(KeyCode.Alpha2))
			StartGame(JsonUtility.ToJson(new JsonStructs.StartGame(Enums.PlayerSide.LEFT, 1f, 0f, -1f, false)));
		if (Input.GetKeyDown(KeyCode.Alpha3))
			GameOver(JsonUtility.ToJson(new JsonStructs.GameOver(Enums.PlayerSide.LEFT, 5, 2, Enums.GameEndStatus.NORNAL)));
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			string data = JsonUtility.ToJson(new JsonStructs.ValidCheckStruct(leftPaddle, rightPaddle, ball));
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	ValidCheck(data);
#endif
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	UnityException("Test Exception");
#endif
		}
	}
}
