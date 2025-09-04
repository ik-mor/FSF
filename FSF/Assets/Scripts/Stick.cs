
using UnityEngine;
using UnityEngine.InputSystem;

public class Stick : MonoBehaviour
{
	[SerializeField] GameObject _player;
	[SerializeField] float moveSpeed = 0f;
	private Gamepad gamepad;



	void Update()
	{
		// 現在接続されているGamepadを取得
		gamepad = Gamepad.current;
		if (gamepad == null) return;

		// 左スティックの入力値を取得
		Vector2 stickInputRight = gamepad.rightStick.ReadValue();

		//移動
		Vector3 movement = new Vector3(stickInputRight.x, 0, stickInputRight.y);
		transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
	}
}
