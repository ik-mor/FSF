using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] GameObject _player;
	[SerializeField] float moveSpeed = 5f;
	private Gamepad gamepad;



	void Update()
	{
		// 現在接続されているGamepadを取得
		gamepad = Gamepad.current;
		if (gamepad == null) return;

		// 左スティックの入力値を取得
		Vector2 stickInputLeft = gamepad.leftStick.ReadValue();

		//移動
		Vector3 movement = new Vector3(stickInputLeft.x, 0, stickInputLeft.y);
		transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
	}
}
