using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public string isDetected;
	public List<string> tags;
	public GameObject closest;

	public ConversationHandler handler;
	public GameObject conversationCanvas;

	private void Awake() {
		conversationCanvas = GameObject.Find("Conversation");
		handler = conversationCanvas.GetComponent<ConversationHandler>();
	}
	
	void Update() {
		if (isDetected.Equals("NPC")) {
			float interact = GameData.controls.movement.Interact.ReadValue<float>();
			CharacterDataHandler chrHandler = closest.GetComponent<CharacterDataHandler>();
			if (interact > 0 && !GameData.isConversing && chrHandler.talkTimer >= chrHandler.talkCooldown) {
				try {
					this.transform.parent.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					handler.startConversation(closest.GetComponent<CharacterDataHandler>().conversation);
				} catch {
					
				}
			}
		} else if (isDetected.Equals("Door")) {
			float interact = GameData.controls.movement.Interact.ReadValue<float>();
			if (interact > 0) {
				try {
					this.transform.parent.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					SceneLoader.loadScene("HomeInside");
				} catch {

				}
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		if (tags.Contains(other.tag)) {
			isDetected = other.tag;
			if (closest == null || Vector3.Distance(closest.transform.position, this.transform.position) > Vector3.Distance(other.transform.position, this.transform.position)) {
				if (closest != null) {
					closest.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
				}
				closest = other.gameObject;
				other.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (tags.Contains(other.tag)) {
			if (closest != null && closest == other.gameObject) {
				closest = null;
			}
			other.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			isDetected = "";
		}
	}
}
