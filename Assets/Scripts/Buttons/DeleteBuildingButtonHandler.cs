using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Обработчик кнопки удалить постройку.")]
public class DeleteBuildingButtonHandler : MonoBehaviour
{
    private Button button;
    private Image buttonImage;
    private Vector3 defaultScale;

    [Tooltip("Singleton скрипта, который обновляет тайлы. Полагается при этом на позицию мыши.")]
    private BuildingCreator buildingCreator;

    private bool isDeletingMode = false; // Состояние режима удаления

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
        isDeletingMode = !isDeletingMode; // Переключаем состояние

        if (isDeletingMode)
        {
            buildingCreator.StartDeleting();
            ApplyPressedState();
        }
        else
        {
            buildingCreator.StopDeleting();
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
