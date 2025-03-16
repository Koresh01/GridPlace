using UnityEngine;
using UnityEngine.InputSystem;  // ��� ������ � ����� �������� ����� Unity.
using UnityEngine.Tilemaps;     // ��� ������ � �������.

[AddComponentMenu("Custom / BuildingCreator (��������� ����� ������)")]
public class BuildingCreator : Singleton<BuildingCreator>
{
    [Tooltip("����� tile-��. ������� � ��� �� ����� �������� �������� ��� Grid.")]
    [SerializeField] Tilemap previewMap;

    PlayerInput playerInput;

    [Tooltip("�������������� ScriptableObject, ������� ���������� �������")]
    BuildingObjectBase selectedObj;
    [Tooltip("Tile ��������� ������� ��� ���������.")]
    TileBase tileBase;

    Camera _camera;

    [Tooltip("������� ���� �� ������")]
    Vector2 mousePos;

    [Tooltip("������� ��������� ������� �� �����")]
    Vector3Int currentGridPosition;

    [Tooltip("���������� ��������� ������� �� �����")]
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
    /// ScriptableObject ��������� ��� ���������.
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
        // ���� ���� ������� ���� �� ������ ��� ���������.
        if (selectedObj != null) { 
            // ������� ������� ���� �� ������, � ������� ����������:
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos); 

            // ����� ����� ���������� �� ����� ������������� ���� �������:
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            // ������� ������ � �� �������� � ������ ������.
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
        
    }

    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        
    }

    /// <summary>
    /// �������� ��������� ������� ��� ���������.
    /// </summary>
    /// <param name="obj">ScriptableObject ��������� ���������.</param>
    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }

    /// <summary>
    /// �������� ���� �� �����.
    /// </summary>
    private void UpdatePreview()
    {
        // ������� ������ ���� ���� ����������
        previewMap.SetTile(lastGridPosition, null);
        // ������������� ������� ���� � ������� ����
        previewMap.SetTile(currentGridPosition, tileBase);

    }
}
