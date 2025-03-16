using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (������� ������.)")]
public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField, Tooltip("�������, ������� ����� ��������.")] BuildingObjectBase fabricItem;
    private Button button;
    private Image buttonImage;
    private Vector3 defaultScale;

    [Tooltip("Singleton �������, ������� ��������� �����. ���������� ��� ���� �� ������� ����.")]
    private BuildingCreator buildingCreator;

    private bool isBuildingMode = false; // ��������� ������ �������������

    [Header("��������� ����������� �����������")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    [SerializeField] private float pressedScaleMultiplier = 0.9f; // ��������� ����������� ������

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>(); // �������� Image ��� ��������� �����
        defaultScale = transform.localScale; // ���������� �������� ������ ������
        buildingCreator = BuildingCreator.GetInstance();

        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        isBuildingMode = !isBuildingMode; // ����������� ���������

        if (isBuildingMode)
        {
            buildingCreator.ObjectSelected(fabricItem); // �������� ������� ��� �������������
            ApplyPressedState();
        }
        else
        {
            buildingCreator.ObjectDeSelected(); // �������� ����� �������
            ResetButtonState();
        }
    }

    private void ApplyPressedState()
    {
        if (buttonImage != null)
            buttonImage.color = pressedColor; // ������ ���� �� �������

        transform.localScale = defaultScale * pressedScaleMultiplier; // ��������� ������
    }

    private void ResetButtonState()
    {
        if (buttonImage != null)
            buttonImage.color = normalColor; // ���������� ������� ����

        transform.localScale = defaultScale; // ���������� ������ ������
    }
}
