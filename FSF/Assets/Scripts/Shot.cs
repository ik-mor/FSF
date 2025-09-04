
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
	private Collider objectCollider;
	private Rigidbody rb;

	public void Gun(Vector3 dir)
	{
		GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 1); //�e�̐F�����ɂ���
		objectCollider = GetComponent<SphereCollider>();
		objectCollider.isTrigger = true; //Trigger�Ƃ��Ĉ���
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false; //�d�͂𖳌��ɂ���
	}
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = 60;
    }

	// Update is called once per frame
}
