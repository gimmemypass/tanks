using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConsoleView : MonoBehaviour
{
	ConsoleController console ;

	public AudioMixer am;
    public GameObject viewContainer;
    public Text logTextArea;
    public InputField inputField;
	// Start is called before the first frame update
	void Start()
	{
		console = gameObject.AddComponent<ConsoleController>();
		console.am = am;
		viewContainer.SetActive(false);
		if (console != null)
		{
			console.visibilityChanged += onVisibilityChanged;
			console.logChanged += onLogChanged;
		}
		onLogChanged(console.log);
	}

	~ConsoleView()
	{
		console.visibilityChanged -= onVisibilityChanged;
		console.logChanged -= onLogChanged;
	}

	public void onVisibilityChanged(bool visible)
	{
		viewContainer.SetActive(visible);
		//inputField.Select();
		inputField.ActivateInputField();
		Pause.PauseGame();
		
	}

	public void onLogChanged(string[] newLog)
	{
		if(newLog == null)
		{
			logTextArea.text = "";
		}
		else
		{
			logTextArea.text = string.Join(" \n", newLog);
		}
	}
	
	private void Update()
	{
		if (Input.GetKeyUp("`"))
		{
			onVisibilityChanged(!viewContainer.activeSelf);
		}

	}
	public void runCommand()
	{
		console.runCommandString(inputField.text);
		inputField.text = "";	
	}
}
