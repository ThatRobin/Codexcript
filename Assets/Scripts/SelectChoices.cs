using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectChoices : MonoBehaviour
{
	public int indicator;
	public int maxIndicator;
	public float moveDelay;

	public float moveTimer;

	public Transform context;
	public GameObject progressConversation;

	void Update() {
		/*
		if (GameData.isConversing) {
			if (transform.gameObject.activeSelf && context.childCount > 0 && this.GetComponent<ConversationHandler>().currentDialogue.Choices.Count > 1) {
				maxIndicator = context.childCount - 1;

				if (moveTimer < moveDelay) {
					moveTimer += Time.deltaTime;
				}

				bool up = (GameData.controls.movement.Y_Axis.ReadValue<float>() == 1);
				bool down = (GameData.controls.movement.Y_Axis.ReadValue<float>() == -1);

				if (down) {
					if (moveTimer >= moveDelay) {
						if (indicator < maxIndicator) {
							indicator++;
						} else {
							indicator = 0;
						}
						moveTimer = 0;
					}
				}

				if (up) {
					if (moveTimer >= moveDelay) {
						if (indicator > 0) {
							indicator--;
						} else {
							indicator = maxIndicator;
						}
						moveTimer = 0;
					}
				}

				if (transform.childCount > 0) {
					//EventSystem.current.SetSelectedGameObject(context.GetChild(indicator).gameObject);
				}
			} else {
				//EventSystem.current.SetSelectedGameObject(progressConversation);
			}
		}
		*/
	}
}
