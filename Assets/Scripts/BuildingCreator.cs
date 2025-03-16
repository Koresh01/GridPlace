using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;  // Для работы с новой системой ввода Unity.
using UnityEngine.Tilemaps;     // Для работы с тайлами.

[AddComponentMenu("Custom / BuildingCreator (Строитель новых тайлов)")]
public class BuildingCreator : Singleton<BuildingCreator>
{
    [Header("Карты tile-ов. Которая у нас на сцене является дочерней для Grid:")]
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
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
            HandleDrawing();
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
    /// В будущем он будет предназначен для рисования линий/квадратов и т.д.
    /// </summary>
    private void HandleDrawing()
    {
        DrawItem();
    }

    /// <summary>
    /// Устанавливает tile фабрики на default слой нашей карты.
    /// </summary>
    private void DrawItem()
    {
        defaultMap.SetTile(currentGridPosition, tileBase);
    }
}
