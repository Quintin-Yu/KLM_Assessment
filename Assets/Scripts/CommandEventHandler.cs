using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class CommandEventHandler : MonoBehaviour
{
    public TMP_InputField inputfield;
    public List<SOCommand> commands;
	
	[SerializeField] private TMP_Text output;
	private Coroutine feedbackRoutine;

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
		if (!input.StartsWith("/")) return;

		string command = input.Substring(1).Trim().ToLower();
		foreach (SOCommand cmd in commands)
		{
			if (cmd.command != command) continue;
			if (feedbackRoutine != null) StopCoroutine(feedbackRoutine);

			feedbackRoutine = StartCoroutine(TextFeedback(5, cmd.output));
			cmd.Invoke();
			break;
		}

		inputfield.text = "";
		inputfield.ActivateInputField();
	}

	public void ExecuteCommand(SOCommand cmd)
	{
		if (commands == null) return;

		if (feedbackRoutine != null) StopCoroutine(feedbackRoutine);
		feedbackRoutine = StartCoroutine(TextFeedback(5, cmd.output));
		cmd.Invoke();
	}

	IEnumerator TextFeedback(float waitTime, string commandOutput)
	{
		output.text = commandOutput;
		yield return new WaitForSeconds(waitTime);
		output.text = "";
	}
}
