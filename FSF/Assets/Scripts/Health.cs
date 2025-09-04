
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField]
	int m_health;

	[SerializeField]
	UnityEvent m_onDeath;

	public int Value
	{
		get => m_health;
	}

	public void Damage(int damage)
	{
		if (damage <= 0) return;    // ���̃_���[�W�͉񕜂��Ă��܂�
		if (m_health <= 0) return;  // ���̏R��͂��Ȃ�

		m_health -= damage;

		// �̗͂������Ȃ����玀�S�ʒm
		if (m_health <= 0)
		{
			m_onDeath?.Invoke();
		}
	}
}
