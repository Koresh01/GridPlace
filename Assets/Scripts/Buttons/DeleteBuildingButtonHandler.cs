using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("���������� ������ ������� ���������.")]
public class DeleteBuildingButtonHandler : MonoBehaviour 
{
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
        buildingCreator.StartDeleting();
    }
}
