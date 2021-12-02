using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    //Список книг которые находяться на полке.
    [SerializeField] private List<Image> _books = null;
    
    private void Start() => this.hideFlags = HideFlags.NotEditable;

    public bool Сheck()
    {
        //Чтобы не создавать список каждый раз,убеждаюсь за ним есть элементы.
        if (_books == null || _books.Count < 2)
        {
            //Получаю все Image(Книги)
            _books = new List<Image>(GetComponentsInChildren<Image>());
            //Удаляю первый элемент,так как это Image самой полки.
            _books.Remove(_books[0]);
        }

        //За ранее указываю что все книги одинакового цвета.
        bool result = true;

        //По цвету первой книги буду проверять остальные книги на этой полке.
        Color _colorBook = _books[0].color;

        _books.ForEach(book =>
        {
            //Если хотя бы одна книга не такого же цвета как первая,прерываю цикл
            // и меняю результат на то что не все книги одинакового цвета.
            if (!book.color.Equals(_colorBook))
            {
                result = false;
                return;
            }
        });

        return result;
    }
}
