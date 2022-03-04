using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _cross;
    [SerializeField] private GameObject _zero;

    public bool IsActive { get; set; } = true;
    public int Index { get; private set; }

    public EntityCellType Type { get; private set; } = EntityCellType.None;

    public void Initialize(int index)
    {
        Index = index;
    }

    public void SelectCross()
    {
        if(!IsActive) return;
        
        _cross.SetActive(true);
        _zero.SetActive(false);
        IsActive = false;
        Type = EntityCellType.Cross;
    }
    
    public void SelectZero()
    {
        if(!IsActive) return;
        
        _cross.SetActive(false);
        _zero.SetActive(true);
        IsActive = false;
        Type = EntityCellType.Zero;
    }

    public void Refresh()
    {
        _cross.SetActive(false);
        _zero.SetActive(false);
        IsActive = true;
        Type = EntityCellType.None;
    }
}
