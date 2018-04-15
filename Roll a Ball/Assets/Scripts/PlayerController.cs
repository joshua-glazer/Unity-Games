using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText, winText, wallText;

	private Rigidbody rb;
	private int count;
	private bool isWin;

	Renderer rend;
	Color32 regColor = new Color32 (0x58, 0xEE, 0xD5, 0xFF);
	Color32 wallColor = new Color32 (0xDA, 0x78, 0x78, 0xFF);
	Color32 winColor = new Color32 (0x0D, 0xFF, 0x37, 0xFF);

	void Start () {
		//physics
		rb = GetComponent<Rigidbody> ();

		//colour stuff
		rend = GetComponent<Renderer> ();
		rend.material.color = regColor;

		// text stuff
		count = 9;
		isWin = false;
		SetCountText ();
		winText.text = ""; wallText.text = "";
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement*speed);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive (false);
			count--;
			SetCountText ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.gameObject.CompareTag("Wall")){
			wallText.text = "Ow!";
			rend.material.color = wallColor;
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.collider.gameObject.CompareTag ("Wall")) {
			wallText.text = "";
			if (isWin) rend.material.color = winColor;
			else rend.material.color = regColor;
		}
	}

	void SetCountText(){
		if (count == 1) {
			countText.text = count.ToString () + "\nBlock\nRemaining";
		}
		else countText.text = count.ToString () + "\nBlocks\nRemaining";

		if (count == 0) {
			winText.text = "You Win!";
			isWin = true;
			rend.material.color = winColor;
		}
	}
}