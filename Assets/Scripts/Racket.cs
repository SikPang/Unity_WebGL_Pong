using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
	enum Side
	{
		Left,
		Right
	};

	[SerializeField] private Side mySide;

	// Update is called once per frame
	void Update()
	{
		if (mySide == Side.Left)
		{
			if (Input.GetKey(KeyCode.W))
				Move(new Vector3(0, 0, 1), 15);
			else if (Input.GetKey(KeyCode.S))
				Move(new Vector3(0, 0, -1), 15);
		}
		else
        {
            if (Input.GetKey(KeyCode.UpArrow))
                Move(new Vector3(0, 0, 1), 15);
            else if (Input.GetKey(KeyCode.DownArrow))
                Move(new Vector3(0, 0, -1), 15);
        }
    }

	void Move(Vector3 dir, float power)
	{
		transform.Translate(dir * power * Time.deltaTime);
	}
}
