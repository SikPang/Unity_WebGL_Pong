using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Detector : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void ScorePoint(string data);

	[SerializeField] Enums.PlayerSide detectorSide;
    Score score;
    Ball ball;

    void Start()
    {
        score = Score.GetInstance();
        ball = Ball.GetInstance();
    }

	void OnCollisionEnter(Collision collision)
	{
        score.GetPoint(detectorSide);
        ball.Initialize();
        string data = JsonUtility.ToJson(detectorSide);
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	ScorePoint(data);
#endif
	}
}
