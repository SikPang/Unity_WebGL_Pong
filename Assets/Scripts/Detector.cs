using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public enum Side
    {
        Left, Right
    }
    [SerializeField] Side mySide;
    Score score;

    void Start()
    {
        score = Score.GetInstance();
    }

	void OnCollisionEnter(Collision collision)
	{
        score.GetPoint(mySide);
	}
}
