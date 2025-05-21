using System.Collections.Generic;
using UnityEngine;
public class DSDialogueSO : ScriptableObject {
	[field: SerializeField] public CharacterData Character { get; set; }
	[field: SerializeField] public string DialogueName { get; set; }
	[field: SerializeField] [field: TextArea()] public string Text { get; set; }
	[field: SerializeField] public List<DSDialogueChoiceData> Choices { get; set; }
	[field: SerializeField] public DSDialogueType DialogueType { get; set; }
	[field: SerializeField] public bool IsStartingDialogue { get; set; }

	public void Initialize(string dialogueName, CharacterData character, string text, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType, bool isStartingDialogue) {
		choices.Reverse();
		DialogueName = dialogueName;
		Character = character;
		Text = text;
		Choices = choices;
		DialogueType = dialogueType;
		IsStartingDialogue = isStartingDialogue;
	}
}