using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject cross;
    [SerializeField] private GameObject zero;
    public bool IsActive { get; set; } = true;
    public int Index { get; private set; }

    public EntityCellType Type { get; private set; } = EntityCellType.None;

    public void Initialize(int index)
    {
        Index = index;
    }

    public void SelectCross()
    {
        if (!IsActive) return;
        
        cross.SetActive(true);
        zero.SetActive(false);
        IsActive = false;
        Type = EntityCellType.Cross;
    }

    public void SelectZero()
    {
        if (!IsActive) return;
        
        cross.SetActive(false);
        zero.SetActive(true);
        IsActive = false;
        Type = EntityCellType.Zero;
    }

    public void Refresh()
    {
        cross.SetActive(false);
        zero.SetActive(false);
        IsActive = true;
        Type = EntityCellType.None;
    }
}