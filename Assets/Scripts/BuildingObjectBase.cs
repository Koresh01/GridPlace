using UnityEngine;
using UnityEngine.Tilemaps;

public enum Category
{
    Fabric, 
    Floor
}

[CreateAssetMenu(fileName = "Buildable", menuName = "BuildingObjects/Create Buildable")]
public class BuildingObjectBase : ScriptableObject
{
    [SerializeField] Category category;
    [SerializeField] TileBase tileBase;

    /// <summary>
    /// Tile(�������� ��������).
    /// </summary>
    public TileBase TileBase
    {
        get
        {
            return tileBase;
        }
    }

    public Category Category
    {
        get
        {
            return category;
        }
    }
}
