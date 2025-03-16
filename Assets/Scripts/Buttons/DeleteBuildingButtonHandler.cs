using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Обработчик кнопки удалить постройку.")]
public class DeleteBuildingButtonHandler : MonoBehaviour 
{
    Button button;
    
    [Tooltip("Singleton скрипта, который обновляет тайлы. Полагается при этом на позицию мыши.")]
    BuildingCreator buildingCreator; // оказывается singleton-ы тоже кэшируют...

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
