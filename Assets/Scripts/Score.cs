using JetBrains.Annotations;
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
	const string youWinText = "You Win !";
	const string youLoseText = "You Lose ..";

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
		string reason = "";
		string target = "";

		if (gos.reason == Enums.GameEndStatus.CHEATING)
			reason = "cheated]";
		else if (gos.reason == Enums.GameEndStatus.DISCONNECT)
			reason = "disconnected]";
		else if (gos.reason == Enums.GameEndStatus.OUTGAME)
			reason = "got lazy]";

		if (gos.winner == GameManager.GetInstance().GetMySide())
		{
			if (gos.reason != Enums.GameEndStatus.NORNAL)
				target = "\n[your opponent ";
			winText.text = youWinText + target + reason;
		}
		else
		{
			if (gos.reason != Enums.GameEndStatus.NORNAL)
				target = "\n[you ";
			winText.text = youLoseText + target + reason;
		}

		leftScoreText.text = gos.leftScore.ToString();
		rightScoreText.text = gos.rightScore.ToString();
		winText.gameObject.SetActive(true);
	}

	public void SetPoint(int leftScore, int rightScore)
	{
		leftScoreText.text = leftScore.ToString();
		rightScoreText.text = rightScore.ToString();
	}

	public void SetTextActive(bool state)
	{
		winText.gameObject.SetActive(state);
	}

	public void SetText(string text)
	{
		winText.text = text;
	}
}
