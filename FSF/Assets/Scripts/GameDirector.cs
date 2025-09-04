
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
	GameObject timerText;
	GameObject hpText;
	GameObject bulletText;
	float time = 60.0f;
	int hp = 3;
	int bullet = 20;
    // Start is called before the first frame update
   

	public void HP()
	{
		this.hp -= 1;
	}

	public void Bullet()
	{
		this.bullet -= 1;
	}
	public void Bullet2()
	{
		this.bullet += 5;
	}

	void Start()
	{
		this.timerText = GameObject.Find("Time");
		this.hpText = GameObject.Find("HP");
		this.bulletText = GameObject.Find("Bullet");
	}

	// Update is called once per frame
	void Update()
    {
		this.time -= Time.deltaTime;

		this.timerText.GetComponent<TextMeshProUGUI>().text =
			this.time.ToString("F1");

		this.hpText.GetComponent<TextMeshProUGUI>().text =
			"HP " + this.hp.ToString();

		this.bulletText.GetComponent<TextMeshProUGUI>().text =
			"Bullet " + this.bullet.ToString();

		if(hp <= 0)
		{
			SceneManager.LoadScene("ClearScene");
		}
		else if(time < 0)
		{
			SceneManager.LoadScene("ClearScene");
		}
	}
}
