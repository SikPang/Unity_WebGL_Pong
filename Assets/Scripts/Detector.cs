using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Detector : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void ScorePoint(Enums.PlayerSide hitSide);

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
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	ScorePoint(detectorSide);
#endif
	}
}
