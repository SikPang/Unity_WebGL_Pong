using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void UnityException(string data);
	
	const string scoreInitText = "0";
	const string leftWinText = "Left player wins!";
	const string rightWinText = "Right player wins!";

	[SerializeField] TextMeshProUGUI leftScoreText;
	[SerializeField] TextMeshProUGUI rightScoreText;
	[SerializeField] TextMeshProUGUI winText;
	static Score instance;

	int leftScore;
	int rightScore;

	private Score() { }

	void Awake()
	{
		Initialize();
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

	public void Finish(JsonStructs.GameOver gos)
	{
		if (gos.winner == Enums.PlayerSide.LEFT)
			winText.text = leftWinText + " " + gos.reason;
		else if (gos.winner == Enums.PlayerSide.RIGHT)
			winText.text = rightWinText + " " + gos.reason;
		else
		{
			// call js function
#if UNITY_WEBGL == true && UNITY_EDITOR == false
			UnityException("Score.Finish() : PlayerSide is NONE");
#endif
		}

		leftScoreText.text = gos.leftScore.ToString();
		rightScoreText.text = gos.rightScore.ToString();
		winText.gameObject.SetActive(true);
	}

	public void GetPoint(Enums.PlayerSide side)
	{
		if (side == Enums.PlayerSide.RIGHT)
		{
			++leftScore;
			leftScoreText.text = leftScore.ToString();
		}
		else
		{
			++rightScore;
			rightScoreText.text = rightScore.ToString();
		}
	}
}
