using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 5f;
	public float turnSpeed = 100f;
	public float move;  //移動
	public float turn;  //回転
						// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		move = Input.GetAxis("Vertical");   //上下の入力を取得します。上が1、下が-1
		turn = Input.GetAxis("Horizontal");  //左右キーの入力を取得します。右が1、左が-1になる。

		transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
		transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
	}
}
