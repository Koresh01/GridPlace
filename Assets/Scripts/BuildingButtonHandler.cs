using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (������ ��� ��������� ������� �� ������ ������ ���������.)")]
public class BuildingButtonHandler : MonoBehaviour 
{
    [SerializeField, Tooltip("�������, ������� ����� ��������.")] BuildingObjectBase fabricItem;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Debug.Log("Button was clicked: " + fabricItem.name);
    }
}
