using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void GameOver();

	static GameManager instance;
	[SerializeField] Paddle leftPaddle;
	[SerializeField] Paddle rightPaddle;
	Score score;
	Ball ball;

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
	}

	public static GameManager GetInstance()
	{
		return instance;
	}

	public void NextGame(bool isFinished)
	{
		if (isFinished)
		{
			ball.Initialize();
			CallGameOver();
			return;
		}
		
		ball.Reset();
		leftPaddle.Initialize();
		rightPaddle.Initialize();
	}

	public void StartGame()
	{
		NextGame(false);
	}

	public void RestartGame()
	{
		score.Initialize();
		NextGame(false);
	}

	public void CallGameOver()
	{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	GameOver();
#endif
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			StartGame();
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			RestartGame();
	}
}
