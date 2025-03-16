using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (������� ������.)")]
public class BuildingButtonHandler : MonoBehaviour 
{
    [SerializeField, Tooltip("�������, ������� ����� ��������.")] BuildingObjectBase fabricItem;
    Button button;

    [Tooltip("Singleton �������, ������� ��������� �����. ���������� ��� ���� �� ������� ����.")]
    BuildingCreator buildingCreator; // ����������� singleton-� ���� ��������...

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        buildingCreator = BuildingCreator.GetInstance();
    }

    private void ButtonClicked()
    {
        Debug.Log("Button was clicked: " + fabricItem.name);
        buildingCreator.ObjectSelected(fabricItem);
    }
}
