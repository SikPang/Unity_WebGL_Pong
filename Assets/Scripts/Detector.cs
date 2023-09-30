using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Detector : MonoBehaviour
{
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
	}
}
