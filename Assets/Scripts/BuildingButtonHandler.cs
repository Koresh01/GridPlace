using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Custom / BuildingButtonHandler (Скрипт для обработки нажатия на кнопку выбора постройки.)")]
public class BuildingButtonHandler : MonoBehaviour 
{
    [SerializeField, Tooltip("Фабрика, которую будем спавнить.")] BuildingObjectBase fabricItem;
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
