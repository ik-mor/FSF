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
		// ���ݐڑ�����Ă���Gamepad���擾
		gamepad = Gamepad.current;
		if (gamepad == null) return;

		// ���X�e�B�b�N�̓��͒l���擾
		Vector2 stickInputLeft = gamepad.leftStick.ReadValue();

		//�ړ�
		Vector3 movement = new Vector3(stickInputLeft.x, 0, stickInputLeft.y);
		transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
	}
}
