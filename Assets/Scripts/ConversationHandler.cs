using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ConversationHandler : MonoBehaviour {

	[SerializeField] private TMP_Text dialogueText;
	[SerializeField] private GameObject dialogueTextObject;
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private GameObject characterTextObject;

	private GameObject npcData2;

	private int index;
    private string actualText = "";

	public float textSpeed;

	public GameObject choiceScreen;
	public GameObject choicePrefab;

	public Transform choiceContent;

	public DSDialogueSO currentDialogue;
	public DSDialogueSO startDialogue;

	private static ConversationHandler instance;

	public static ConversationHandler GetInstance() {
		return instance;
	}
	
	public void Start() {
		if (instance == null) instance = this;
		
	}
	
	public void startConversation(DSDialogueSO startingDialogue) {
		//npcData2 = npcData;
		GameData.isConversing = true;
		currentDialogue = startingDialogue;
		this.transform.GetChild(0).gameObject.SetActive(true);
		showText();
	}

	private void Update() {
		if(currentDialogue != null) {
			choiceScreen.SetActive(currentDialogue.Choices.Count > 1);
		}
	}


	private void clearChoices() {
		for (int i = 0; i < choiceContent.childCount; i++) {
			Destroy(choiceContent.GetChild(i).gameObject);
		}
	}

	private void createChoices() {
		clearChoices();
		List<DSDialogueChoiceData> choices = currentDialogue.Choices;

		foreach(DSDialogueChoiceData choice in choices) {
			GameObject choiceTemp = Instantiate(choicePrefab) as GameObject;
			choiceTemp.transform.SetParent(choiceContent, false);
			RectTransform rect = choiceTemp.GetComponent<RectTransform>();
			rect.localPosition = new Vector2(0, rect.rect.height * (choices.IndexOf(choice) - (choices.Count+1)));
			choiceContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, choiceContent.GetComponent<RectTransform>().rect.height + rect.rect.height);
			choiceTemp.GetComponent<ChoiceHandler>().handler = this;
			choiceTemp.GetComponentInChildren<TMP_Text>().text = choice.Text;
		}
	}

	void showText() {
		dialogueText.text = Sign.SGetText();

		if (index < currentDialogue.Text.Length) {
			char letter = currentDialogue.Text[index];

			dialogueText.text = writeLetter(letter);

			index += 1;
			StartCoroutine(pauseBetweenChars(letter));
		} else {
			if (currentDialogue.Choices.Count > 1) {
				createChoices();
			}
		}

		characterTextObject.SetActive(currentDialogue.Character.chrName != "");
		
		characterText.text = currentDialogue.Character.chrName;
		characterText.transform.parent.gameObject.GetComponent<Image>().color = currentDialogue.Character.characterColour;
	}

	private string writeLetter(char letter) {
		actualText += letter;
		return actualText;
	}

	private IEnumerator pauseBetweenChars(char letter) {
		if(letter != ' ') {
			yield return new WaitForSeconds(10 - textSpeed);
		}
		showText();
		yield break;
	}

	public void onOptionSelect(int choiceIndex) {
		actualText = "";
		index = 0;

		DSDialogueSO nextDialogue = currentDialogue.Choices[choiceIndex].NextDialogue;

		if (nextDialogue == null) {
			GameData.isConversing = false;
			//npcData2.GetComponent<CharacterDataHandler>().talkTimer = 0;
			this.transform.GetChild(0).gameObject.SetActive(false);
		} else {
			currentDialogue = nextDialogue;

			clearChoices();
			showText();
		}
	}
}
