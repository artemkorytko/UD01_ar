using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityCellType _playWith = EntityCellType.None;
    [SerializeField] protected EntityType _playerType = EntityType.None;

    public abstract void GetStep(params EntityCellType[] field);

    public System.Action<int, EntityCellType> OnStep = null;
}

public enum EntityType
{
    None,
    Player,
    Bot
}

public enum EntityCellType
{
    None,
    Cross,
    Zero
}

