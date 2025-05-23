using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DSRootNode : DSNode {
	public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position) {
		base.Initialize(nodeName, dsGraphView, position);

		DialogueType = DSDialogueType.SingleChoice;

		DSChoiceSaveData choiceData = new DSChoiceSaveData() {
			Text = "Next Dialogue"
		};

		Choices.Add(choiceData);
	}

	public override void Draw() {
		base.Draw();

		/* OUTPUT CONTAINER */

		foreach (DSChoiceSaveData choice in Choices) {
			Port choicePort = this.CreatePort(choice.Text);

			choicePort.userData = choice;

			outputContainer.Add(choicePort);
		}

		RefreshExpandedState();
	}
}
