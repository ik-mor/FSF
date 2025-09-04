
using UnityEngine;
using UnityEngine.InputSystem;

public class Stick : MonoBehaviour
{
	[SerializeField] GameObject _player;
	[SerializeField] float moveSpeed = 0f;
	private Gamepad gamepad;



	void Update()
	{
		// ���ݐڑ�����Ă���Gamepad���擾
		gamepad = Gamepad.current;
		if (gamepad == null) return;

		// ���X�e�B�b�N�̓��͒l���擾
		Vector2 stickInputRight = gamepad.rightStick.ReadValue();

		//�ړ�
		Vector3 movement = new Vector3(stickInputRight.x, 0, stickInputRight.y);
		transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
	}
}
