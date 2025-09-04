
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

	[SerializeField] LayerMask ignoreLayer;         // 無視していいレイヤー

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
		GameObject newbullet = Instantiate(m_bullet, m_gun.transform.position, Quaternion.identity); //弾を生成
		Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
		Ray ray = playCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
		RaycastHit hit;

		Vector3 targetPoint;       //タップした場所

		if (Physics.Raycast(ray, out hit, m_bulletSpeed, ~ignoreLayer)) // レイが何に当たったかをチェック
			targetPoint = hit.point;

		else targetPoint = ray.GetPoint(10);// 何にも当たらなかったらレイの長さを強制決定

		// 銃口から見たターゲットの方向を取得
		Vector3 BulletWith = targetPoint - m_shotpoint.position;

		float x = Random.Range(-m_shot, m_shot);
		float y = Random.Range(-m_shot, m_shot);

		Vector3 BulletWithout = BulletWith + new Vector3(x, y, 0);

		// 弾を前方に向かせる
		newbullet.transform.forward = BulletWithout.normalized;

		bulletRigidbody.AddForce(BulletWithout.normalized * m_bulletSpeed); //キャラクターが向いている方向に弾に力を加える
		Destroy(newbullet, 5); //5秒後に弾を消す
	}

	// Update is called once per frame
	void Update()
    {
		// タイマーの時間を動かす
		timer += Time.deltaTime;
	}
}
