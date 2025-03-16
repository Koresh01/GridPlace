using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (Стройка зданий.)")]
public class BuildingButtonHandler : MonoBehaviour 
{
    [SerializeField, Tooltip("Фабрика, которую будем спавнить.")] BuildingObjectBase fabricItem;
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
        Debug.Log("Button was clicked: " + fabricItem.name);
        buildingCreator.ObjectSelected(fabricItem);
    }
}
