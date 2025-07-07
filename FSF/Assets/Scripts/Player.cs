using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 5f;
	public float turnSpeed = 100f;
	public float move;  //�ړ�
	public float turn;  //��]
						// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		move = Input.GetAxis("Vertical");   //�㉺�̓��͂��擾���܂��B�オ1�A����-1
		turn = Input.GetAxis("Horizontal");  //���E�L�[�̓��͂��擾���܂��B�E��1�A����-1�ɂȂ�B

		transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
		transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
	}
}
