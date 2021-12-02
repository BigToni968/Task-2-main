using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Book : MonoBehaviour
{
    //���� ���� ��������� ����� ���� �������.
    private GameLogic _gameLogic = null;
    //��� ���� �������� ����� ���� �������.
    private Image _book = null;
    //��� ���������.
    private EventTrigger _trigger = null;

    private void Awake()
    {
        _book = _book ?? GetComponent<Image>();
        _trigger = _trigger ?? GetComponent<EventTrigger>();
    }
    void Start()
    {
        _gameLogic = _gameLogic ?? GameObject.FindObjectOfType<GameLogic>();
        //�������� ������� ��� ���������.
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        //��� ������� ����� ����� ����� �������.
        entry.callback.AddListener((data) => _gameLogic.SelectBook(_book));
        //�������� ������� ���������.
        _trigger.triggers.Add(entry);
    }
}
