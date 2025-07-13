using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class CommandEventHandler : MonoBehaviour
{
    public TMP_InputField inputfield;
    public List<SOCommand> commands;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inputfield == null)
        {
            throw new MissingReferenceException($"No inputfield was found in {gameObject.name}");
        }

        inputfield.onEndEdit.AddListener(HandleInput);
    }
    
    private void HandleInput(string input)
    {
		if (string.IsNullOrWhiteSpace(input)) return;

		if (input.StartsWith("/"))
		{
			string command = input.Substring(1).Trim().ToLower();
			foreach (SOCommand cmd in commands)
			{
				if (cmd.command == command)
				{
					Debug.Log($"CEH = {cmd.output}");
					cmd.Invoke();
					break;
				}
			}
		}

		inputfield.text = "";
		inputfield.ActivateInputField();
	}
}
