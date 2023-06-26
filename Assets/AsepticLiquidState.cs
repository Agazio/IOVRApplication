using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsepticLiquidState : ATask
{
    [SerializeField] private CottonInteraction _cottonInteraction;

    [SerializeField] private GameObject[] _handGrabs;

    private TasksManager _simulation; 

    private void Start()
    {
        //_description = "Grab the antiseptic solution and soak a cotton ball.";
        _simulation = GetComponentInParent<TasksManager>();
    }

    public override void OnEntry(TasksManager controller)
    {
        controller.DisableButton();

        foreach(GameObject handGrab in _handGrabs)
        {
            handGrab.SetActive(true);
        }

        _tts.SpeakQueued(_speakingText);
        _virtualAssistantText.text = _speakingText;
    }

    public override void OnExit(TasksManager controller)
    {
        //_simulation.AddTaskTimestamp();
    }

    public override void OnUpdate(TasksManager controller)
    {
        if(_cottonInteraction.isAbsorbing() && !_isCompleted)
        {
            _isCompleted = true;
            controller.EnableButton();
            controller.PlayTaskCompletedSound();
        }
    }
}
