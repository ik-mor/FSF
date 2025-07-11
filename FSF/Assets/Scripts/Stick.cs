using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stick : MonoBehaviour
{
	[SerializeField]
	float m_speed = 8;

	Vector3 m_direction;
	Vector3 m_velocity;
	PlayerInput m_playerInput;
	Rigidbody m_rigidBody;
	Health m_health;

	bool m_canMove;
	bool m_canDirection;
	bool m_isDeath;

	private void Awake()
	{
		m_playerInput = GetComponent<PlayerInput>();
		m_rigidBody = GetComponent<Rigidbody>();
		m_health = GetComponent<Health>();
	}

	private void Start()
	{
		m_direction = new Vector3(0, 0, 0);
		m_velocity = new Vector3(0, 0, 0);
		m_canMove = true;
		m_canDirection = true;
		m_isDeath = false;
	}

	private void OnEnable()
	{
		m_playerInput.actions["Move"].performed += OnMove;
		m_playerInput.actions["Move"].canceled += OnMoveCancel;
	}

	private void OnDisable()
	{
		m_playerInput.actions["Move"].performed -= OnMove;
		m_playerInput.actions["Move"].canceled -= OnMoveCancel;
	}

	private void OnMove(InputAction.CallbackContext callback)
	{
		var value = callback.ReadValue<Vector2>();
		m_direction = new Vector3(value.x, 0, value.y);
	}

	void OnMoveCancel(InputAction.CallbackContext callback)
	{
		m_direction = Vector3.zero;
	}

	// アニメーションから呼ばれる
	public void ResetTrigger()
	{
		m_canMove = true;
		m_canDirection = true;
	}

	private void FixedUpdate()
	{
		if (m_isDeath) return;

		// カメラの正面ベクトルを作成
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		if (m_canDirection)
		{
			// カメラの向きを考慮した移動量
			m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
			m_velocity *= m_speed;
		}

		// 進行方向にゆっくり向く
		if (m_velocity != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(m_velocity.normalized), 0.1f);
		}

		// 移動
		if (m_canMove)
		{
			// 重力による落下を保持
			m_velocity.y = m_rigidBody.velocity.y;

			m_rigidBody.velocity = m_velocity;
		}
	}

	public void OnDeath()
	{
		m_isDeath = true;
		m_playerInput.enabled = false;
	}
}
