using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    //Чтобы не писать лишний раз скрипт переходов,
    //то сдесь сразу и переход и рестарт сцены.
    //Переход по имени. Но класс можно расширять под удобства.
    [SerializeField] string nameNextScen = null;
    void Start()
    {
        //Если забыл указать имя сцены для перехода,то чтобы не было ошибки,
        //то беру имя этой сцены и лучше её перезащу при вызове прехода.
        if (string.IsNullOrEmpty(nameNextScen))
            nameNextScen = SceneManager.GetActiveScene().name;
    }

    public void Next() => SceneManager.LoadScene(nameNextScen);

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
