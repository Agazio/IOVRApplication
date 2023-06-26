using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdhesive_State : ATask
{
    [SerializeField] private Sticker _sticker1;
    [SerializeField] private Sticker _sticker2;
    [SerializeField] private GameObject[] _handGrabs;

    public override void OnEntry(TasksManager controller)
    {
        _isCompleted = false;
        controller.DisableButton();
        foreach (GameObject handGrab in _handGrabs)
        {
            handGrab.SetActive(true);
        }
        _tts.SpeakQueued(_speakingText);
        _virtualAssistantText.text = _speakingText;
    }

    public override void OnExit(TasksManager controller)
    {
    }

    public override void OnUpdate(TasksManager controller)
    {
        if (!_sticker1.IsAttached() && !_sticker2.IsAttached() && !_isCompleted)
        {
            _isCompleted = true;
            controller.EnableButton();
            controller.PlayTaskCompletedSound();
        }
    }
}
