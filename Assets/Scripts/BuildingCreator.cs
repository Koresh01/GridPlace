using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;  // Для работы с новой системой ввода Unity.
using UnityEngine.Tilemaps;     // Для работы с тайлами.

[AddComponentMenu("Custom / BuildingCreator (Строитель новых тайлов)")]
public class BuildingCreator : Singleton<BuildingCreator>
{
    [Header("Карты tile-ов. Которая у нас на сцене является дочерней для Grid:")]
    [SerializeField] Tilemap initialMap;    // При удалении tile построенного здания, исходный тайл берем отсюда.
    [SerializeField] Tilemap defaultMap;
    [SerializeField] Tilemap previewMap;

    PlayerInput playerInput;

    [Tooltip("Закэшированный ScriptableObject, который собираемся строить")]
    BuildingObjectBase selectedObj;
    [Tooltip("Tile выбранной фабрики для постройки.")]
    TileBase tileBase;

    Camera _camera;

    [Tooltip("Позиция мыши на экране")]
    Vector2 mousePos;

    [Tooltip("Текущее положение курсора на сетке")]
    Vector3Int currentGridPosition;

    [Tooltip("Предыдущее положение курсора на сетке")]
    Vector3Int lastGridPosition;

    [Tooltip("Флаг режима удаления.")]
    private bool isDeleting = false;


    protected override void Awake()
    {
        base.Awake();

        playerInput = new PlayerInput();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.GamePlay.MousePosition.performed += OnMouseMove;
        playerInput.GamePlay.MouseLeftClick.performed += OnLeftClick;
        playerInput.GamePlay.MouseRightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.GamePlay.MousePosition.performed -= OnMouseMove;
        playerInput.GamePlay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.GamePlay.MouseRightClick.performed -= OnRightClick;
    }

    /// <summary>
    /// ScriptableObject выбранный для постройки.
    /// </summary>
    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;

            tileBase = selectedObj != null ? selectedObj.TileBase : null;
            UpdatePreview();
        }
    }

    private void Update()
    {
        // Если была выбрана одна из фабрик для постройки.
        if (selectedObj != null) { 
            // Перевод позиции мыши на экране, в мировые координаты:
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos); 

            // Узнаём какие координаты на сетке соответствуют этой позиции:
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            // Двигали курсор и он выскочил в другую клетку.
            if (gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                UpdatePreview();
            }
        }

        // Если собираемся удалять.
        if (isDeleting)
        {
            // Перевод позиции мыши на экране, в мировые координаты:
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            // Узнаём какие координаты на сетке соответствуют этой позиции:
            currentGridPosition = previewMap.WorldToCell(pos);
        }
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isDeleting)
                HandleDeletion(); // Если включен режим удаления — удаляем
            else if (selectedObj != null)
                DrawItem(); ; // Если выбрана фабрика — строим
        }
    }

    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        SelectedObj = null;
    }

    /// <summary>
    /// Кэширует выбранную фабрику для постройки.
    /// </summary>
    /// <param name="obj">ScriptableObject выбранной постройки.</param>
    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }
    /// <summary>
    /// Отчищает здание для постройки.
    /// </summary>
    /// <param name="obj">ScriptableObject выбранной постройки.</param>
    public void ObjectDeSelected()
    {
        SelectedObj = null;
    }

    /// <summary>
    /// Заменяет тайл на сетке.
    /// </summary>
    private void UpdatePreview()
    {
        // Удаляем старый тайл если существует
        previewMap.SetTile(lastGridPosition, null);
        // Устанавливает текущий тайл в позицию мыши
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    /// <summary>
    /// Устанавливает tile фабрики на default слой нашей карты.
    /// </summary>
    private void DrawItem()
    {
        defaultMap.SetTile(currentGridPosition, tileBase);
    }





    #region Удаление
    /// <summary>
    /// Включает режим удаления зданий.
    /// </summary>
    public void StartDeleting()
    {
        isDeleting = true;
        SelectedObj = null; // Сбрасываем выбор объекта для строительства
    }

    /// <summary>
    /// Выключает режим удаления зданий.
    /// </summary>
    public void StopDeleting()
    {
        isDeleting = false;
        SelectedObj = null; // Сбрасываем выбор объекта для строительства
    }

    /// <summary>
    /// Заменяет текущий тайл на тайл из initialMap.
    /// </summary>
    private void HandleDeletion()
    {
        TileBase initialTile = initialMap.GetTile(currentGridPosition);
        defaultMap.SetTile(currentGridPosition, initialTile);
    }
    #endregion

}
