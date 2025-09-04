
using UnityEngine;

public class GunController : MonoBehaviour
{

	[SerializeField]
	float m_bulletSpeed = 500;
	[SerializeField]
	float m_shot;

	[SerializeField] Transform m_shotpoint;

	[SerializeField] GameObject m_bullet;
	[SerializeField] GameObject m_gun;

	[SerializeField] GameObject playCam;

	[SerializeField] LayerMask ignoreLayer;         // �������Ă������C���[

	private float timeBetweenShot = 0.35f;
	private float timer;
	void Start()
    {
		
	}

	public void Gun(int _bulletCount)
	{
		if (_bulletCount >= 1 && timer > timeBetweenShot)
		{
			timer = 0.0f;
			Bullet();
		}		
	}

	public void Bullet()
	{
		GameObject newbullet = Instantiate(m_bullet, m_gun.transform.position, Quaternion.identity); //�e�𐶐�
		Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
		Ray ray = playCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
		RaycastHit hit;

		Vector3 targetPoint;       //�^�b�v�����ꏊ

		if (Physics.Raycast(ray, out hit, m_bulletSpeed, ~ignoreLayer)) // ���C�����ɓ������������`�F�b�N
			targetPoint = hit.point;

		else targetPoint = ray.GetPoint(10);// ���ɂ�������Ȃ������烌�C�̒�������������

		// �e�����猩���^�[�Q�b�g�̕������擾
		Vector3 BulletWith = targetPoint - m_shotpoint.position;

		float x = Random.Range(-m_shot, m_shot);
		float y = Random.Range(-m_shot, m_shot);

		Vector3 BulletWithout = BulletWith + new Vector3(x, y, 0);

		// �e��O���Ɍ�������
		newbullet.transform.forward = BulletWithout.normalized;

		bulletRigidbody.AddForce(BulletWithout.normalized * m_bulletSpeed); //�L�����N�^�[�������Ă�������ɒe�ɗ͂�������
		Destroy(newbullet, 5); //5�b��ɒe������
	}

	// Update is called once per frame
	void Update()
    {
		// �^�C�}�[�̎��Ԃ𓮂���
		timer += Time.deltaTime;
	}
}
