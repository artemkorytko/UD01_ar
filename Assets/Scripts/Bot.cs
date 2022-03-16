using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Entity
{
    public override void GetStep(params EntityCellType[] field)
    {
        StartCoroutine(ChooseStep(field));
    }

    private IEnumerator ChooseStep(EntityCellType[] field)
    {
        yield return new WaitForSeconds(0.5f);

        List<int> freeCells = new List<int>();

        for (int i = 0; i < field.Length; i++)
        {
            if (field[i] == EntityCellType.None)
            {
                freeCells.Add(i);
            }
        }

        OnStep?.Invoke(freeCells[Random.Range(0, freeCells.Count)], _playWith);
    }
}