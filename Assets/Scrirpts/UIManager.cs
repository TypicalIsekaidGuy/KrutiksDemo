using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null; // Ёкземпл€р объекта
    [SerializeField]private Button[] buttons;
    [SerializeField]private Sprite[] pressedButtons;
    [SerializeField]private Sprite[] activeButtons;

    public Button royPunch;
    public Button solCut;
    public GameObject Joystick;
    void Start()
    {
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

        // “еперь нам нужно указать, чтобы объект не уничтожалс€
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

        // » запускаем собственно инициализатор
        InitializeManager();
    }

    // ћетод инициализации менеджера
    private void InitializeManager()
    {
        /* TODO: «десь мы будем проводить инициализацию */
    }
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void SetButtonActive(int i)
    {
        foreach (var button in buttons)
            button.interactable = true;
        buttons[i].interactable = false;
    }
    public void ActivateButton(Button button)
    {
        button.interactable = true;
    }
    public void DeactivateButton(Button button)
    {
        button.interactable = false;
    }
    public void ActivateJoystick()
    {
        Joystick.SetActive(true);
    }
    public void DeactivateJoystick()
    {
        Joystick.SetActive(false);
    }
}