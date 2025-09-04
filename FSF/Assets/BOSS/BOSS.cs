using System.Collections;

using UnityEngine;

public class BOSS : MonoBehaviour
{
	[SerializeField]
	GameObject[] m_skills;

	[SerializeField]
	ParticleSystem m_castEffect;

	[SerializeField]
	float m_secondHalfHealthPer = 50;

	[Header("SoundEffect")]

	[SerializeField]
	AudioClip m_audioSkillCast;

	float m_speed = 2;

	float m_waitTime;

	Animator m_animator;
	Health m_health;

	int m_maxHealth;
	int m_timelinePhase;
	int m_timelineId;
	double m_timelineTime;

	Transform m_player;
	int m_stackCastNum;
	bool m_isDeath;

	enum TimelineActionId
	{
		Message = 0,
		Skill = 1,
		Loop = 99,
	}

	private void Start()
	{
		//m_player = GameObject.FindWithTag("Player").transform;
		m_animator = GetComponent<Animator>();
		m_health = GetComponent<Health>();
		m_stackCastNum = 0;
		m_isDeath = false;

		m_maxHealth = m_health.Value;

		m_timelinePhase = 0;
		m_timelineId = 0;
		m_timelineTime = 0;
	}

	private void FixedUpdate()
	{
		if (m_isDeath) return;

		// �^�C�����C��
		m_timelineTime += Time.deltaTime;

		if (m_stackCastNum > 0) return;
	}

	public void OnDeath()
	{
		// ���S�A�j���[�V�����Đ�
		AnimationCrossFade("Die", 0.25f);

		// �r���G�t�F�N�gOFF
		m_castEffect.GetComponent<ParticleSystem>().Stop();

		// ���S�t���O
		m_isDeath = true;
	}

	void AnimationCrossFade(string stateName, float duration)
	{
		if (m_isDeath) return;
		m_animator.CrossFade(stateName, duration);
	}

	//IEnumerator SetupSkill(GameObject skill)
	//{
	//	EnemySkill enemySkill = skill.GetComponent<EnemySkill>();
	//
	//	// UI
	//	m_castUI.Setup(enemySkill.SkillName, enemySkill.CastTime);
	//
	//	// SE
	//	SoundEffect.Play2D(m_audioSkillCast, 0.25f, 0.5f);
	//
	//	// �r���A�j���[�V����
	//	StartCoroutine(CastMotion(enemySkill.CastTime, enemySkill.FinishCastAnimeName));
	//
	//	// �X�L���̗\���͈͂�\������܂őҋ@
	//	yield return new WaitForSeconds(enemySkill.DelayTime);
	//
	//	// �X�L���n���i�\���͈͂̕\���j
	//	Instantiate(skill, transform.position, transform.rotation);
	//}

	IEnumerator CastMotion(float castTime, string finishCastAnimeName)
	{
		m_stackCastNum++;

		// �r���A�j���[�V�����Đ�
		AnimationCrossFade("Cast", 0.05f);

		// �r���G�t�F�N�gON
		m_castEffect.GetComponent<ParticleSystem>().Play();

		// �r���҂�
		yield return new WaitForSeconds(castTime);

		// �r���G�t�F�N�gOFF
		m_castEffect.GetComponent<ParticleSystem>().Stop();

		// �r�������A�j���[�V�����Đ�
		AnimationCrossFade(finishCastAnimeName, 0.25f);

		yield return new WaitForSeconds(2.0f);

		m_stackCastNum--;
	}
}
