using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystemUI : MonoBehaviour
{
    [SerializeField] private CanvaAnimator _canvas;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Image _messageIcon;
    [SerializeField] private float _duration;
    private bool _hasDuration = true;

    [Header("Icons Type")]
    [SerializeField] private Sprite _warningIcon;
    [SerializeField] private Sprite _notificationIcon;
    [SerializeField] private Sprite _suggestionIcon;


    Message message = new Message();
    private bool _isActive = false;
    public class Message
    {
        public string Text = "Insert message here.";
        public MessageType Type = MessageType.NOTIFICATION;
    }

    public static MessageSystemUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public MessageSystemUI SetMessage(string text)
    {
        message.Text = text;
        return Instance; 
    }

    public MessageSystemUI SetType(MessageType type)
    {
        message.Type = type;
        return Instance;
    }

    public MessageSystemUI SetDuration(float duration)
    {
        _duration = duration;
        return Instance;
    }

    public void Show(bool hasDuration)
    {
        _hasDuration = hasDuration;
        _messageText.text = message.Text;
        switch (message.Type)
        {
            case MessageType.NOTIFICATION:
                _messageIcon.sprite = _notificationIcon;
                break;
            case MessageType.WARNING:
                _messageIcon.sprite = _warningIcon;
                break;
            case MessageType.SUGGESTION:
                _messageIcon.sprite = _suggestionIcon;
                break;
        }
        _messageIcon.color = Color.black;

        _isActive = true;
        _canvas.ActivateCanvas();
        if (_hasDuration)
        {
            StartCoroutine(HideCoroutine());
        }
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        Hide();
    }

    public void Hide()
    {
        _isActive = false;
        _canvas.HideCanvas();
        message = new Message();
    }

    public bool IsActive()
    {
        return _isActive;
    }
}

public enum MessageType
{
    WARNING,
    NOTIFICATION,
    SUGGESTION
}
