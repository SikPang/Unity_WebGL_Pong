using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	const string scoreInitText = "0";
	const string leftWinText = "Left player wins!";
	const string rightWinText = "Right player wins!";
	const int maxScore = 5;

	[SerializeField] TextMeshProUGUI leftScoreText;
	[SerializeField] TextMeshProUGUI rightScoreText;
	[SerializeField] TextMeshProUGUI winText;
	static Score instance;
	GameManager gameManager;

	int leftScore;
	int rightScore;

	void Awake()
	{
		Initialize();
	}

	void Start()
	{
		gameManager = GameManager.GetInstance();
	}

	public void Initialize()
	{
		instance = this;
		leftScore = 0;
		rightScore = 0;
		leftScoreText.text = scoreInitText;
		rightScoreText.text = scoreInitText;
		winText.gameObject.SetActive(false);
	}

	public static Score GetInstance()
	{
		return instance;
	}

	bool CheckFinish()
	{
		if (leftScore == maxScore ||  rightScore == maxScore) 
		{
			if (leftScore > rightScore)
				winText.text = leftWinText;
			else
				winText.text = rightWinText;
			winText.gameObject.SetActive(true);
			return true;
		}
		return false;
	}

	public void GetPoint(Detector.Side side)
	{
		if (side == Detector.Side.Right)
		{
			++leftScore;
			leftScoreText.text = leftScore.ToString();
		}
		else
		{
			++rightScore;
			rightScoreText.text = rightScore.ToString();
		}
		gameManager.NextGame(CheckFinish());
	}
}
