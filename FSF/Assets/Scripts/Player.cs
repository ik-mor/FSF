
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField]
	float m_speed = 8;

	[SerializeField]
	int m_maxbullet = 20;

	[SerializeField]
	int m_bullet;

	[SerializeField]
	GameObject m_gun;

	Vector3 m_direction;
	Vector3 m_velocity;
	PlayerInput m_playerInput;
	Rigidbody m_rigidBody;
	Health m_health;
	

	bool m_canMove;
	bool m_canDirection;
	bool m_isDeath;

	//�����������ǂ����̃t���O
	bool isHit;

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

		m_bullet = m_maxbullet;
	}

	private void OnEnable()
	{

		m_playerInput.actions["Move"].performed += OnMove;
		m_playerInput.actions["Move"].canceled += OnMoveCancel;

		m_playerInput.actions["Attack"].performed += OnAttack;
	}

	private void OnDisable()
	{
		m_playerInput.actions["Move"].performed -= OnMove;
		m_playerInput.actions["Move"].canceled -= OnMoveCancel;

		m_playerInput.actions["Attack"].performed -= OnAttack;
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

	public void OnAttack(InputAction.CallbackContext callback)
	{
		m_gun.GetComponent<GunController>().Gun(m_bullet);
	}

	// �A�j���[�V��������Ă΂��
	public void ResetTrigger()
	{
		m_canMove = true;
		m_canDirection = true;
	}

	private void FixedUpdate()
	{
		if (m_isDeath) return;

		// �J�����̐��ʃx�N�g�����쐬
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		if (m_canDirection)
		{
			// �J�����̌������l�������ړ���
			m_velocity = cameraForward * m_direction.z + Camera.main.transform.right * m_direction.x;
			m_velocity *= m_speed;
		}

		// �i�s�����ɂ���������
		if (m_velocity != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(m_velocity.normalized), 0.3f);
		}

		// �ړ�
		if (m_canMove)
		{
			// �d�͂ɂ�闎����ێ�
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
