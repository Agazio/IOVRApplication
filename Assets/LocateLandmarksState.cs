using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateLandmarksState : ATask
{
    [SerializeField] private MessageSystemUI _messageSys;

    private void Start()
    {
        //_description = "Locate insertion site: two finger breadths below the patella and 1-2cm medial to the tibial tuberosity.";
    }

    public override void OnEntry(TasksManager controller)
    {
        controller.DisableButton();
        StartCoroutine(Wait(5f, controller));
        _tts.SpeakQueued(_speakingText);
        _virtualAssistantText.text = _speakingText;
        _messageSys.SetMessage("If you don't remember the correct insertion point you can ask the virtual assistant to show it.").SetType(MessageType.SUGGESTION).Show(false);
    }

    public override void OnExit(TasksManager controller)
    {
        _messageSys.Hide();
    }

    private IEnumerator Wait(float seconds, TasksManager controller)
    {
        yield return new WaitForSeconds(seconds);
        controller.EnableButton();
    }

    public override void OnUpdate(TasksManager controller)
    {
     
    }
}
