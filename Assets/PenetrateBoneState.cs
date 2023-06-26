using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBoneState : ATask
{
    [SerializeField] private PowerDrill _drill;

    private TasksManager _simulation;
    [SerializeField] private MessageSystemUI _messageSys; 

    private void Start()
    {
        //_description = "Penetrate the bone cortex by squeezing the driver’s trigger and applying gentle, consistent, downward pressure.";
        _simulation = GetComponentInParent<TasksManager>();
    }

    public override void OnEntry(TasksManager controller)
    {
        _isCompleted = false;
        controller.DisableButton();
        _tts.SpeakQueued(_speakingText);
        _virtualAssistantText.text = _speakingText;
    }

    public override void OnExit(TasksManager controller)
    {
        NeedleInteraction needle = _drill.GetNeedle().GetComponent<NeedleInteraction>();
        float positionPrecision = needle.GetPositionPrecision();
        float inclinationPrecision = needle.GetAnglePrecision();
        _simulation.SetPositionPrecision(positionPrecision);
        _simulation.SetInclinationPrecision(inclinationPrecision);
        Debug.Log("Position precision " + positionPrecision + " has been saved correctly!");
        Debug.Log("Inclination precision " + inclinationPrecision + " has been saved correctly!");
    }

    public override void OnUpdate(TasksManager controller)
    {
        if (_drill.GetNeedle().GetComponent<NeedleInteraction>().GetCurrentState() == State.MEDULLARY_CAVITY && !_isCompleted)
        {
            _isCompleted = true;
            controller.EnableButton();
            controller.PlayTaskCompletedSound();
            _messageSys.SetMessage("The tip of the needle has successfully reached the medullary cavity of the bone!").SetType(MessageType.NOTIFICATION).Show();

        }
        else if(_drill.GetNeedle().GetComponent<NeedleInteraction>().GetCurrentState() != State.MEDULLARY_CAVITY)
        {
            _isCompleted = false;
            controller.DisableButton();
        }
    }
}
