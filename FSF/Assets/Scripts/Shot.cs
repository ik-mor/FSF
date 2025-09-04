
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
	private Collider objectCollider;
	private Rigidbody rb;

	public void Gun(Vector3 dir)
	{
		GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 1); //’e‚ÌF‚ğ•‚É‚·‚é
		objectCollider = GetComponent<SphereCollider>();
		objectCollider.isTrigger = true; //Trigger‚Æ‚µ‚Äˆµ‚¤
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false; //d—Í‚ğ–³Œø‚É‚·‚é
	}
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = 60;
    }

	// Update is called once per frame
}
