using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    public override void GetStep(params EntityCellType[] field)
    {
        StartCoroutine(ChooseStep());
    }

    private IEnumerator ChooseStep()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPosition = Input.mousePosition;
                Ray ray = _camera.ScreenPointToRay(touchPosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    Cell cell = raycastHit.collider.gameObject.GetComponent<Cell>();

                    if (cell != null && cell.IsActive)
                    {
                        OnStep?.Invoke(cell.Index, _playWith);
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}