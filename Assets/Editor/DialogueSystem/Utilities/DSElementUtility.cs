using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public static class DSElementUtility {
	public static Button CreateButton(string text, Action onClick = null) {
		Button button = new Button(onClick) {
			text = text
		};
		return button;
	}

	public static Foldout CreateFoldout(string title, bool collapsed = false) {
		Foldout foldout = new Foldout() {
			text = title,
			value = !collapsed
		};
		return foldout;
	}

	public static Port CreatePort(this DSNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single) {
		Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

		port.portName = portName;

		return port;
	}

	public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null) {
		TextField textField = new TextField() {
			value = value,
			label = label
		};

		if (onValueChanged != null) {
			textField.RegisterValueChangedCallback(onValueChanged);
		}

		return textField;
	}

	public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null) {
		TextField textArea = CreateTextField(value, label, onValueChanged);

		textArea.multiline = true;

		return textArea;
	}

	public static ObjectField CreateObjectField(CharacterData value = null, string label = null, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null) {
		ObjectField objectField = new ObjectField() {
			value = value,
			objectType = typeof(CharacterData),
			label = label
		};

		if(onValueChanged != null) {
			objectField.RegisterValueChangedCallback(onValueChanged);
		}

		return objectField;
	}
}