using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceHandler : MonoBehaviour
{
	public ConversationHandler handler;
	GameObject canvas;

	private void Awake() {
		canvas = GameObject.Find("Conversation");
		handler = canvas.GetComponent<ConversationHandler>();
	}

	public void OnClick() {
		handler.onOptionSelect((transform.parent.childCount-1) - transform.GetSiblingIndex());
	}

	public void progressConversation() {
		handler.onOptionSelect(0);
	}
}
