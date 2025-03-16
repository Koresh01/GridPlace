using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (Стройка зданий.)")]
public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Фабрика, которую будем спавнить.")] BuildingObjectBase fabricItem;
    private Button button;
    private Image buttonImage;
    private Vector3 defaultScale;

    [Tooltip("Singleton скрипта, который обновляет тайлы. Полагается при этом на позицию мыши.")]
    private BuildingCreator buildingCreator;

    private bool isBuildingMode = false; // Состояние режима строительства

    [Header("Настройки визуального отображения")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    [SerializeField] private float pressedScaleMultiplier = 0.9f; // Насколько уменьшается кнопка

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>(); // Получаем Image для изменения цвета
        defaultScale = transform.localScale; // Запоминаем исходный размер кнопки
        buildingCreator = BuildingCreator.GetInstance();

        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        isBuildingMode = !isBuildingMode; // Переключаем состояние

        if (isBuildingMode)
        {
            buildingCreator.ObjectSelected(fabricItem); // Выбираем фабрику для строительства
            ApplyPressedState();
        }
        else
        {
            buildingCreator.ObjectDeSelected(); // Отчищаем выбор фабрики
            ResetButtonState();
        }
    }

    private void ApplyPressedState()
    {
        if (buttonImage != null)
            buttonImage.color = pressedColor; // Меняем цвет на нажатый

        transform.localScale = defaultScale * pressedScaleMultiplier; // Уменьшаем размер
    }

    private void ResetButtonState()
    {
        if (buttonImage != null)
            buttonImage.color = normalColor; // Возвращаем обычный цвет

        transform.localScale = defaultScale; // Возвращаем размер кнопки
    }
}
