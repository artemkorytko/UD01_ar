using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Field : MonoBehaviour
{
  private Cell[] _cells;

  public event Action<EntityCellType> OnCompleted;
  
  private void Awake()
  {
    _cells = GetComponentsInChildren<Cell>(true);
  }

  public void Initialize()
  {
    for (int i = 0; i < _cells.Length; i++)
    {
      _cells[i].Initialize(i);
    }
  }

  public void Refresh()
  {
    for (int i = 0; i < _cells.Length; i++)
    {
      _cells[i].Refresh();
    }
  }

  public void SetCell(int index, EntityCellType cellType)
  {
    if(index < 0 || index >= _cells.Length)
      return;

    switch (cellType)
    {
      case EntityCellType.Cross:
        _cells[index].SelectCross();
        break;
      case EntityCellType.Zero:
        _cells[index].SelectZero();
        break;

    }

    Check();
  }

  private void Check()
  {
    if (CheckLines(out EntityCellType winType))
    {
      OnCompleted?.Invoke(winType);
      return;
    }

    if (CheckCount())
    {
      OnCompleted?.Invoke(EntityCellType.None);
    }
  }

  private bool CheckCount()
  {
    int unusedCount = 0;
    for (int i = 0; i < _cells.Length; i++)
    {
      if (_cells[i].Type == EntityCellType.None)
      {
        unusedCount++;
      }
    }

    if (unusedCount == 0)
      return true;
    
    
    return false;
    
    
  }

  private bool CheckLines(out EntityCellType winCell)
  {
    winCell = default;
    EntityCellType cellType;
    
    // Check rows
    for (int i = 0; i < _cells.Length; i+=3)
    {
      cellType = _cells[i].Type;
      bool result = true;
      
      for (int j = i+1; j < i+3; j++)
      {
        if (cellType != _cells[j].Type)
        {
          result = false;
          break;
        }
      }

      if (result && cellType != EntityCellType.None)
      {
        winCell = cellType;
        return true;
      }
    }
    
    // Check columns
    for (int i = 0; i < 3; i++)
    {
      cellType = _cells[i].Type;
      bool result = true;
      for (int j = i+3; j < _cells.Length; j+=3)
      {
        if (cellType != _cells[j].Type)
        {
          result = false;
          break;
        }
      }

      if (result && cellType != EntityCellType.None)
      {
        winCell = cellType;
        return true;
      }
    }
    
    // Check diagonal 1
    {
      cellType = _cells[0].Type;
      bool result = true;
      for (int i = 4; i < _cells.Length; i+=4)
      {
        if (cellType != _cells[i].Type)
        {
          result = false;
            break;
        }
      }
      if (result && cellType != EntityCellType.None)
      {
        winCell = cellType;
        return true;
      }
    }
    
    // Check diagonal 2
    {
      cellType = _cells[2].Type;
      bool result = true;
      for (int i = 4; i < _cells.Length-1; i+=2)
      {
        if (cellType != _cells[i].Type)
        {
          result = false;
          break;
        }
      }
      if (result && cellType != EntityCellType.None)
      {
        winCell = cellType;
        return true;
      }
    }
    
    return false;
  }
}
