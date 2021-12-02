using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GManager : MonoBehaviour
{
    //���� ����� ������ ����� ��� �������� �����,���� � �� ��������� �� ����� ����.

    //����� ��� ���� ����� ��������� �� ��� ����� � �����
    //��� ��� �� �������� ����������� ������ Scroll View
    [SerializeField] private GameObject _content = null;
    //�������
    [SerializeField] private Shelf _shelfPrefab = null;
    [SerializeField] private Image _bookPrefab = null;
    //����� ��� ������� ��������� �����,���� � �� �����.
    [SerializeField] private List<SetShelf> _setShelf = null;

    //������ ��� ������ � ��� �������� �������,������� � �������.
    private List<Image> _books = null;
    private List<Shelf> _shelfs = null;

    //����� ��� GameLogic. ��� ��� ��� �����������,�������� � ������ ������ �������� � ����.
    public List<Shelf> Shelfs => _shelfs;

    private void Awake()
    {
        //��� ��� �� ����� �����,�� �� ������ ������ ��� ���.
        //�� ���� �������� ����� ���������,�� ���������� �������� ��� ����� � ������ �� ������.
        //����� ����� ����� ������ ���� ������ � ������ ����������.
        _content = _content ?? GameObject.Find("Content");
        //���������� �����,��� ���������� ����� � ����������.
        _shelfs = new List<Shelf>(_setShelf.Count);
        //��� ��� � ���������� ������ ���� 6,�� ������ ���������� ����� � ���������� ���������
        //�� ������������ ���������� ����. �������,����� � ���������� ����� 
        //�� ����������� � ������� ���� ������������� ������.
        _books = new List<Image>(_setShelf.Count * 6);

        //�������� ����� � �� ���������� �� ��������.
        _setShelf.ForEach(sh =>
        {
            if (_content != null) //���� ���� ������.
                if (_shelfPrefab != null) //���� ����� ����� ������.
                    if (_bookPrefab != null) //���� ����� ����� ������ �� �����.
                    {
                        //�������� ����� � �� Scroll View
                        Shelf shelf = Instantiate(_shelfPrefab,_content.transform);
                        //���������� � ������ ����� ����� ����� ��������� ���� � ��� ���������� �����.
                        _shelfs.Add(shelf);

                        //�� ������ ���� ���-�� ����� �� ��� � ���������� ����.
                        //� ��������� ��� �������� � ���� �� ������� �������� 0,
                        //���� ��� �������� �� 2 - 6.
                        int countBook = sh.CountBook < 2 ? 2 : sh.CountBook;

                        //�������� ���� �� ������.
                        for (int i = 0; i < countBook; i++)
                        {
                            Image book = Instantiate(_bookPrefab, shelf.transform);
                            //���� �� ����� ��������� �� ��������� �� ��� ����� �� �����.
                            book.color = sh.ColorBook;
                            //��� ����� �������� ����� ������ ��� ������� �����������.
                            _books.Add(book);
                        }
                    }
        });

        RandomColor();
    }

    private void Start() {}

    private void RandomColor()
    {
        //��� ���������������� ����� ����. �� ���� �����!
        _books.ForEach(book =>
        {
            //���� ������ �� ������ �� ��������� �����. 
            int rand = Random.Range(1,_books.Count);
            //����� ����� ���� ���� ����� � ����� �� ��� ���� �
            //������ 5 ����� � 4 ������� �������.
            Color color = _books[rand].color;
            _books[rand].color = _books[rand -1].color;
            _books[rand -1].color = color;
        });

        //������� ������ ��� ��� ����� �� ������ ������.
        bool thisReturn = false;

        //�������� �� �� ��� ��� ����� ����������.
        //���� ������������ �� �������,�� ����� ��������.
        _shelfs.ForEach(sh =>
        {
            if (sh.�heck())
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
    //����� ��������� ����� � ����.
    [SerializeField][Range(2,6)] private int _countBook;
    [SerializeField] private Color _colorBook;

    public int CountBook => _countBook;
    public Color ColorBook => _colorBook;
}
