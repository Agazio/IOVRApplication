using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verify5mmState : ATask
{
    private TasksManager _simulation;

    [SerializeField] private PowerDrill _drill;
    [SerializeField] private MessageSystemUI _messageSys;

    private Needle _needle; 

    private void Start()
    {
        //_description = "Confirm that at least one black line on the catheter is visible, otherwise the needle is too short and larger size should be used";
        _simulation = GetComponentInParent<TasksManager>();
    }

    public override void OnEntry(TasksManager controller)
    {
        controller.DisableButton();
        _tts.SpeakQueued(_speakingText);
        _virtualAssistantText.text = _speakingText;
        _needle = _drill.GetNeedle();
        StartCoroutine(Wait(5f, controller));
    }

    public override void OnExit(TasksManager controller)
    {
        //_simulation.AddTaskTimestamp();
    }

    public override void OnUpdate(TasksManager controller)
    {

    }

    public override int GetNextTaskId(int currentTask)
    {
        NeedleInteraction needle = _drill.GetNeedle().GetComponent<NeedleInteraction>();
        int nextTaskId;
        if(_needle.GetNeedleType() == NeedleType.BLUE && needle.IsOneLineVisible() )
        {
            nextTaskId = currentTask + 1;
            _messageSys.SetMessage("You've chosen the correct sized needle, at least one black line (5mm) is visible.").SetType(MessageType.NOTIFICATION).Show();
        }
        else
        {
            nextTaskId = 6;
            _messageSys.SetMessage("You've chosen the wrong sized needle, choose another needle to proceed with the Intraosseous Insertion.").SetType(MessageType.WARNING).Show();
        }
        Debug.Log("Is One line visible: " + needle.IsOneLineVisible());
        Debug.Log("Needle state: " + needle.GetCurrentState());
        return nextTaskId;
    }

    private IEnumerator Wait(float seconds, TasksManager controller)
    {
        yield return new WaitForSeconds(seconds);
        controller.EnableButton();
    }
}
