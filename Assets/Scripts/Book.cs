using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Book : MonoBehaviour
{
    //Куда надо постучать когда тебя выберут.
    private GameLogic _gameLogic = null;
    //Что надо передать когда тебя выберут.
    private Image _book = null;
    //Сам слушатель.
    private EventTrigger _trigger = null;

    private void Awake()
    {
        _book = _book ?? GetComponent<Image>();
        _trigger = _trigger ?? GetComponent<EventTrigger>();
    }
    void Start()
    {
        _gameLogic = _gameLogic ?? GameObject.FindObjectOfType<GameLogic>();
        //Создание события для слушателя.
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        //Что вызвать когда когда будет событие.
        entry.callback.AddListener((data) => _gameLogic.SelectBook(_book));
        //Добавить событие слушателю.
        _trigger.triggers.Add(entry);
    }
}
