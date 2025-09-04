
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
		if (damage <= 0) return;    // •‰‚Ìƒ_ƒ[ƒW‚Í‰ñ•œ‚µ‚Ä‚µ‚Ü‚¤
		if (m_health <= 0) return;  // Ž€‘ÌR‚è‚Í‚µ‚È‚¢

		m_health -= damage;

		// ‘Ì—Í‚ª–³‚­‚È‚Á‚½‚çŽ€–S’Ê’m
		if (m_health <= 0)
		{
			m_onDeath?.Invoke();
		}
	}
}
