using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //Тут отображеться  движение книги,отображение шагов и проверка с отображение победы или проигрыша.

    //Нужен для того чтобы вывести уведомление об проигрыше или победе.
    [SerializeField] private Image _Notifications = null;
    //Отображает количество оставшихся шагов.
    [SerializeField] private Text _Step = null;
    //Нужен для визуального перемещения книги. Заранее висит на сцене,смотреть на Canvas.
    [SerializeField] private Image _bookTmp = null;
    //Выбор цвета для выделения выбранной книги.
    [SerializeField] private Color _colorSelected = Color.green;
    //Сколько даёться шагов на эту сцену.
    [SerializeField] private int _countStep = 10;
    //Скорость замены книг.
    [SerializeField] private float _speedAnimation = 4;
    
    //Список выбранных книг. Так как замена происходит межу 2 книгами,то и размер такой.
    private List<Image> _selectedBook = new List<Image>(2);
    //Так как обе книги будут выделены,то их цвета заранее будут храниться тут.
    private List<Color> _colorsSelectedBooks = new List<Color>(2);
    //Для визуальной активации замены книг.
    private bool _revers = false;
    //Нужен для получения полок.
    private GManager _GManager = null;

    //Заранее вывод сколько у игрока есть шагов.
    private void Awake() => _Step.text = _countStep.ToString();

    //Поиск Менеджера.
    void Start() => _GManager = _GManager ?? GameObject.FindObjectOfType<GManager>();

    public void SelectBook(Image book)
    {
        //Выбор книги. Этот метод вызывается слушателем на самой книге.
        if (!_revers)
        {
            //Если визуальная замена книг не началась,то выбираем книгу,выделяя это цветом.
            _colorsSelectedBooks.Add(book.color);
            book.color = _colorSelected;

            //Если выбрали одну и ту же книгу
            if (_selectedBook.Contains(book))
            {
                //Вернуть книге цвет её цвет до выделения.
                book.color = _colorsSelectedBooks[0];
                //Убрать книгу из списка выбранных.
                _selectedBook.Remove(book);
                //Убрать из списка и цвет выбраной книги.
                _colorsSelectedBooks.RemoveAll(colorSelected => colorSelected.Equals(colorSelected));
            }
            else //Если выбрали две разные книги.
                _selectedBook.Add(book);

            //2 книги выбраны,значит пора действовать.
            if (_selectedBook.Count == 2)
                setBookTmp(_revers = true);

        }
    }

    private void FixedUpdate()
    {
        //Если замена началась.
        if (_revers)
            Animation();
    }

    private void Animation()
    {
        //Перемещаем пустышку ко второй книге.
        _bookTmp.transform.position = Vector3.MoveTowards(_bookTmp.transform.position, _selectedBook[1].transform.position, _speedAnimation * Time.deltaTime);
        //Убеждаемся что пустышка на месте.
        _revers =!  (_bookTmp.transform.position == _selectedBook[1].transform.position);
        //Если пустышка достигла второй книги.
        if (!_revers)
        {
            //Выключаем пустышку,влючаем первую книгу.
            setBookTmp(false);
            //Обмен цветов у книг.
            ReversColor( _selectedBook[0],_selectedBook[1]);
            //Очитска списка выбранных книг.
            _selectedBook.Clear();
            //Вывод оставшихся шагов.
            DrawStep();
        }
    }

    private void setBookTmp(bool active)
    {
        //Настройка книги которая будет визуализировать замену.

        //Координаты первой книге передаются пустышке.
        _bookTmp.transform.position = _selectedBook[0].transform.position;
        //Пустышка включается если active == true;
        _bookTmp.gameObject.SetActive(active);
        //Первая книга выключается если active == true.
        //Это надо для повторного переключения пустшыки и первой книги.
        _selectedBook[0].gameObject.SetActive(!active);
        //Если цвет пустышки не совпадает с цветом выделения,то
        if (!_bookTmp.color.Equals(_colorSelected))
            _bookTmp.color = _colorSelected; //то теперь совпадает.
        //Пустышка получает размеры первой книги.
        _bookTmp.rectTransform.sizeDelta = _selectedBook[0].rectTransform.sizeDelta;
    }

    //Метод через который книги менются цветами.
    private void ReversColor(Image a, Image b)
    {
        a.color = _colorsSelectedBooks[1];
        b.color = _colorsSelectedBooks[0];
        //После замены хранить цвета нет смысла.
        _colorsSelectedBooks.Clear();
    }

    //Метод для отображения шагов.
    private void DrawStep()
    {
        _countStep--;
        _Step.text = _countStep.ToString();
        GameOver();
    }

    //Метод для проверки победы или проигрыша.
    //Вызываеться после каждого шага.
    private void GameOver()
    {
        //Проверка оставшихся шагов.
        if (_countStep <= 0)
            Notifications("You Lose");

        //Изначально верим что победили.
        bool win = true;

        //Провереям все книги на одинаковый цвет на их полках.
        _GManager.Shelfs.ForEach(sh =>
        {
            if (!sh.Сheck())
            {
                //Если нет,не считаеться за победу.
                win = false;
                return;
            }
        });

        //Если победили.
        if (win)
            Notifications("You Win");
    }

    //Метод для вывода сообщения.
    private void Notifications(string mess)
    {
        _Notifications.gameObject.SetActive(true);
        //Позволил себе такое,потому что этот метод на сцене вызываеться крайне редко.
        _Notifications.GetComponentInChildren<Text>().text = mess;
    }
}
