using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _fieldPrefab;
    [SerializeField] private ARRaycastManager _arRaycastManager;
    [SerializeField] private ARPlaneManager _arPlaneManager;
    [SerializeField] private ARAnchorManager _arAnchorManager;
    [SerializeField] private GameObject _completedScreen;

    private Player _player;
    private Bot _bot;

    private bool _isEntityStepped;

    private Field _fieldInstance;
    private FieldPositionController _fieldPositionController;
    private FieldSettings _fieldSettings;

    private Coroutine _gameCoroutine;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _bot = GetComponent<Bot>();
        
        _fieldInstance = Instantiate(_fieldPrefab, transform).GetComponent<Field>();
        _fieldInstance.Initialize();
        _fieldInstance.Refresh();
        _fieldInstance.OnCompleted += OnCompleted;

        _fieldSettings = _fieldInstance.GetComponent<FieldSettings>();
        _fieldPositionController = GetComponent<FieldPositionController>();
        _fieldPositionController.Initialize(_arRaycastManager, _arPlaneManager, _arAnchorManager);
        _fieldPositionController.IsActive = true;
    }

    public void StartGame()
    {
        _gameCoroutine = StartCoroutine(GameCoroutine());
    }

    private IEnumerator GameCoroutine()
    {
        Entity currentEntity;

        while (true)
        {
            currentEntity = _player;
            currentEntity.OnStep += OnStep;
            _isEntityStepped = false;
            currentEntity.GetStep();
            
            yield return new WaitWhile(() => _isEntityStepped == false);
            currentEntity.OnStep -= OnStep;
            
            currentEntity.OnStep += OnStep;
            _isEntityStepped = false;
            currentEntity.GetStep();
            
            yield return new WaitWhile(() => _isEntityStepped == false);
            currentEntity.OnStep -= OnStep;
        }
    }

    private void OnStep(int index, EntityCellType type)
    {
        _fieldInstance.SetCell(index, type);
        _isEntityStepped = true;
    }

    private void OnCompleted(EntityCellType type)
    {
        
    }
}
