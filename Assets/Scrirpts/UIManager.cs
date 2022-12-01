using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null; // ��������� �������
    [SerializeField]private Button[] buttons;
    [SerializeField]private Sprite[] pressedButtons;
    [SerializeField]private Sprite[] activeButtons;
    // �����, ����������� ��� ������ ����
    void Start()
    {
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance == this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }

        // ������ ��� ����� �������, ����� ������ �� �����������
        // ��� �������� �� ������ ����� ����
        DontDestroyOnLoad(gameObject);

        // � ��������� ���������� �������������
        InitializeManager();
    }

    // ����� ������������� ���������
    private void InitializeManager()
    {
        /* TODO: ����� �� ����� ��������� ������������� */
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
}