using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    //������ ���� ������� ���������� �� �����.
    [SerializeField] private List<Image> _books = null;
    
    private void Start() => this.hideFlags = HideFlags.NotEditable;

    public bool �heck()
    {
        //����� �� ��������� ������ ������ ���,��������� �� ��� ���� ��������.
        if (_books == null || _books.Count < 2)
        {
            //������� ��� Image(�����)
            _books = new List<Image>(GetComponentsInChildren<Image>());
            //������ ������ �������,��� ��� ��� Image ����� �����.
            _books.Remove(_books[0]);
        }

        //�� ����� �������� ��� ��� ����� ����������� �����.
        bool result = true;

        //�� ����� ������ ����� ���� ��������� ��������� ����� �� ���� �����.
        Color _colorBook = _books[0].color;

        _books.ForEach(book =>
        {
            //���� ���� �� ���� ����� �� ������ �� ����� ��� ������,�������� ����
            // � ����� ��������� �� �� ��� �� ��� ����� ����������� �����.
            if (!book.color.Equals(_colorBook))
            {
                result = false;
                return;
            }
        });

        return result;
    }
}
