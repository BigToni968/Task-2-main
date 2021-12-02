using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //��� ������������  �������� �����,����������� ����� � �������� � ����������� ������ ��� ���������.

    //����� ��� ���� ����� ������� ����������� �� ��������� ��� ������.
    [SerializeField] private Image _Notifications = null;
    //���������� ���������� ���������� �����.
    [SerializeField] private Text _Step = null;
    //����� ��� ����������� ����������� �����. ������� ����� �� �����,�������� �� Canvas.
    [SerializeField] private Image _bookTmp = null;
    //����� ����� ��� ��������� ��������� �����.
    [SerializeField] private Color _colorSelected = Color.green;
    //������� ������ ����� �� ��� �����.
    [SerializeField] private int _countStep = 10;
    //�������� ������ ����.
    [SerializeField] private float _speedAnimation = 4;
    
    //������ ��������� ����. ��� ��� ������ ���������� ���� 2 �������,�� � ������ �����.
    private List<Image> _selectedBook = new List<Image>(2);
    //��� ��� ��� ����� ����� ��������,�� �� ����� ������� ����� ��������� ���.
    private List<Color> _colorsSelectedBooks = new List<Color>(2);
    //��� ���������� ��������� ������ ����.
    private bool _revers = false;
    //����� ��� ��������� �����.
    private GManager _GManager = null;

    //������� ����� ������� � ������ ���� �����.
    private void Awake() => _Step.text = _countStep.ToString();

    //����� ���������.
    void Start() => _GManager = _GManager ?? GameObject.FindObjectOfType<GManager>();

    public void SelectBook(Image book)
    {
        //����� �����. ���� ����� ���������� ���������� �� ����� �����.
        if (!_revers)
        {
            //���� ���������� ������ ���� �� ��������,�� �������� �����,������� ��� ������.
            _colorsSelectedBooks.Add(book.color);
            book.color = _colorSelected;

            //���� ������� ���� � �� �� �����
            if (_selectedBook.Contains(book))
            {
                //������� ����� ���� � ���� �� ���������.
                book.color = _colorsSelectedBooks[0];
                //������ ����� �� ������ ���������.
                _selectedBook.Remove(book);
                //������ �� ������ � ���� �������� �����.
                _colorsSelectedBooks.RemoveAll(colorSelected => colorSelected.Equals(colorSelected));
            }
            else //���� ������� ��� ������ �����.
                _selectedBook.Add(book);

            //2 ����� �������,������ ���� �����������.
            if (_selectedBook.Count == 2)
                setBookTmp(_revers = true);

        }
    }

    private void FixedUpdate()
    {
        //���� ������ ��������.
        if (_revers)
            Animation();
    }

    private void Animation()
    {
        //���������� �������� �� ������ �����.
        _bookTmp.transform.position = Vector3.MoveTowards(_bookTmp.transform.position, _selectedBook[1].transform.position, _speedAnimation * Time.deltaTime);
        //���������� ��� �������� �� �����.
        _revers =!  (_bookTmp.transform.position == _selectedBook[1].transform.position);
        //���� �������� �������� ������ �����.
        if (!_revers)
        {
            //��������� ��������,������� ������ �����.
            setBookTmp(false);
            //����� ������ � ����.
            ReversColor( _selectedBook[0],_selectedBook[1]);
            //������� ������ ��������� ����.
            _selectedBook.Clear();
            //����� ���������� �����.
            DrawStep();
        }
    }

    private void setBookTmp(bool active)
    {
        //��������� ����� ������� ����� ��������������� ������.

        //���������� ������ ����� ���������� ��������.
        _bookTmp.transform.position = _selectedBook[0].transform.position;
        //�������� ���������� ���� active == true;
        _bookTmp.gameObject.SetActive(active);
        //������ ����� ����������� ���� active == true.
        //��� ���� ��� ���������� ������������ �������� � ������ �����.
        _selectedBook[0].gameObject.SetActive(!active);
        //���� ���� �������� �� ��������� � ������ ���������,��
        if (!_bookTmp.color.Equals(_colorSelected))
            _bookTmp.color = _colorSelected; //�� ������ ���������.
        //�������� �������� ������� ������ �����.
        _bookTmp.rectTransform.sizeDelta = _selectedBook[0].rectTransform.sizeDelta;
    }

    //����� ����� ������� ����� ������� �������.
    private void ReversColor(Image a, Image b)
    {
        a.color = _colorsSelectedBooks[1];
        b.color = _colorsSelectedBooks[0];
        //����� ������ ������� ����� ��� ������.
        _colorsSelectedBooks.Clear();
    }

    //����� ��� ����������� �����.
    private void DrawStep()
    {
        _countStep--;
        _Step.text = _countStep.ToString();
        GameOver();
    }

    //����� ��� �������� ������ ��� ���������.
    //����������� ����� ������� ����.
    private void GameOver()
    {
        //�������� ���������� �����.
        if (_countStep <= 0)
            Notifications("You Lose");

        //���������� ����� ��� ��������.
        bool win = true;

        //��������� ��� ����� �� ���������� ���� �� �� ������.
        _GManager.Shelfs.ForEach(sh =>
        {
            if (!sh.�heck())
            {
                //���� ���,�� ���������� �� ������.
                win = false;
                return;
            }
        });

        //���� ��������.
        if (win)
            Notifications("You Win");
    }

    //����� ��� ������ ���������.
    private void Notifications(string mess)
    {
        _Notifications.gameObject.SetActive(true);
        //�������� ���� �����,������ ��� ���� ����� �� ����� ����������� ������ �����.
        _Notifications.GetComponentInChildren<Text>().text = mess;
    }
}
