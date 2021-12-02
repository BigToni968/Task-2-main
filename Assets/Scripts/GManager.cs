using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GManager : MonoBehaviour
{
    //Этот класс создан чисто для создания полок,книг и их настройки до самой игры.

    //Нужен для того чтобы закрепить за ним полки и книги
    //Так как он является контейнером внутри Scroll View
    [SerializeField] private GameObject _content = null;
    //Префабы
    [SerializeField] private Shelf _shelfPrefab = null;
    [SerializeField] private Image _bookPrefab = null;
    //Класс для удобной настройки полок,книг и их цвета.
    [SerializeField] private List<SetShelf> _setShelf = null;

    //Данные для работы с уже готовыми цветами,полками и книгами.
    private List<Image> _books = null;
    private List<Shelf> _shelfs = null;

    //Нужен для GameLogic. Так как там отображение,проверка и прочие мелочи заметные в игре.
    public List<Shelf> Shelfs => _shelfs;

    private void Awake()
    {
        //Так как он очень важен,то на всякий случай ищу его.
        //Но если скроллов будет несколько,то желательно накинуть ему класс и искать по классу.
        //Такой поиск хорош только если объект в едином экземпляре.
        _content = _content ?? GameObject.Find("Content");
        //Количество полок,это количество полок в настройках.
        _shelfs = new List<Shelf>(_setShelf.Count);
        //Так как в настройках предел книг 6,то берётся количество полок в настройках умноженое
        //на максимальное количество книг. Причина,совет в интеренете чтобы 
        //по возможности у списков были фексированная длинна.
        _books = new List<Image>(_setShelf.Count * 6);

        //Создание полок и их заполнение из настроек.
        _setShelf.ForEach(sh =>
        {
            if (_content != null) //Есть куда вешать.
                if (_shelfPrefab != null) //Есть полки чтобы вешать.
                    if (_bookPrefab != null) //Есть книги чтобы вешать на полки.
                    {
                        //Создание полки и на Scroll View
                        Shelf shelf = Instantiate(_shelfPrefab,_content.transform);
                        //Добавление в список полок чтобы потом проверять есть в них одинаковые книги.
                        _shelfs.Add(shelf);

                        //На случай если что-то пойдёт не так с настройкой книг.
                        //В редакторе при создании у меня по дефолту выбивало 0,
                        //Хотя там ограниче от 2 - 6.
                        int countBook = sh.CountBook < 2 ? 2 : sh.CountBook;

                        //Создание книг на полках.
                        for (int i = 0; i < countBook; i++)
                        {
                            Image book = Instantiate(_bookPrefab, shelf.transform);
                            //Цвет за ранее стваиться из настройки на все книги на полке.
                            book.color = sh.ColorBook;
                            //Все книги попадают общий список для будущей перетасовки.
                            _books.Add(book);
                        }
                    }
        });

        RandomColor();
    }

    private void Start() {}

    private void RandomColor()
    {
        //Тут перетасовываются цвета книг. Не сами книги!
        _books.ForEach(book =>
        {
            //Беру предел от второй до последней книги. 
            int rand = Random.Range(1,_books.Count);
            //Затем меняю цвет этой книги и книги на ряд ниже её
            //Пример 5 книга и 4 менются цветами.
            Color color = _books[rand].color;
            _books[rand].color = _books[rand -1].color;
            _books[rand -1].color = color;
        });

        //Заранее считаю что все книги на полках разные.
        bool thisReturn = false;

        //Проверка на на что все книги одинаковые.
        //Если Рандомизация не помогла,то вызов рекурсии.
        _shelfs.ForEach(sh =>
        {
            if (sh.Сheck())
            {
                thisReturn = true;
                return;
            }
        });

        if (thisReturn)
            RandomColor();
    }
}

[System.Serializable]
public class SetShelf
{
    //Класс настройки полок и книг.
    [SerializeField][Range(2,6)] private int _countBook;
    [SerializeField] private Color _colorBook;

    public int CountBook => _countBook;
    public Color ColorBook => _colorBook;
}
