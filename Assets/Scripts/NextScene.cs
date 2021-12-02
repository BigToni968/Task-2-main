using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    //����� �� ������ ������ ��� ������ ���������,
    //�� ����� ����� � ������� � ������� �����.
    //������� �� �����. �� ����� ����� ��������� ��� ��������.
    [SerializeField] string nameNextScen = null;
    void Start()
    {
        //���� ����� ������� ��� ����� ��� ��������,�� ����� �� ���� ������,
        //�� ���� ��� ���� ����� � ����� � �������� ��� ������ �������.
        if (string.IsNullOrEmpty(nameNextScen))
            nameNextScen = SceneManager.GetActiveScene().name;
    }

    public void Next() => SceneManager.LoadScene(nameNextScen);

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
