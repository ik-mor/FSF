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

		// タイムライン
		m_timelineTime += Time.deltaTime;

		if (m_stackCastNum > 0) return;
	}

	public void OnDeath()
	{
		// 死亡アニメーション再生
		AnimationCrossFade("Die", 0.25f);

		// 詠唱エフェクトOFF
		m_castEffect.GetComponent<ParticleSystem>().Stop();

		// 死亡フラグ
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
	//	// 詠唱アニメーション
	//	StartCoroutine(CastMotion(enemySkill.CastTime, enemySkill.FinishCastAnimeName));
	//
	//	// スキルの予兆範囲を表示するまで待機
	//	yield return new WaitForSeconds(enemySkill.DelayTime);
	//
	//	// スキル始動（予兆範囲の表示）
	//	Instantiate(skill, transform.position, transform.rotation);
	//}

	IEnumerator CastMotion(float castTime, string finishCastAnimeName)
	{
		m_stackCastNum++;

		// 詠唱アニメーション再生
		AnimationCrossFade("Cast", 0.05f);

		// 詠唱エフェクトON
		m_castEffect.GetComponent<ParticleSystem>().Play();

		// 詠唱待ち
		yield return new WaitForSeconds(castTime);

		// 詠唱エフェクトOFF
		m_castEffect.GetComponent<ParticleSystem>().Stop();

		// 詠唱完了アニメーション再生
		AnimationCrossFade(finishCastAnimeName, 0.25f);

		yield return new WaitForSeconds(2.0f);

		m_stackCastNum--;
	}
}
