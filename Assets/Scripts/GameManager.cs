using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fieldPrefab;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private ARAnchorManager arAnchorManager;
    [SerializeField] private GameObject completedScreen;

    private Player _player;
    private Bot _bot;

    private bool _isEntityMakeStep;

    private Field _fieldIntance;
    private FieldPositionController _fieldPositionController;
    private FieldSettings _fieldSettings;

    private Coroutine _gameCoroutine;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _bot = GetComponent<Bot>();

        _fieldIntance = Instantiate(fieldPrefab).GetComponent<Field>();
        _fieldIntance.Initialize();
        _fieldIntance.Refresh();
        _fieldIntance.OnCompleted += OnCompleted;

        _fieldSettings = _fieldIntance.GetComponent<FieldSettings>();

        _fieldPositionController = _fieldIntance.GetComponent<FieldPositionController>();
        _fieldPositionController.Initialize(arRaycastManager, arPlaneManager, arAnchorManager);
        _fieldPositionController.IsActive = true;
    }

    private void OnCompleted(EntityCellType cellType)
    {
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
            currentEntity.OnStep += OnEntityStep;
            _isEntityMakeStep = false;
            currentEntity.GetStep();

            yield return new WaitWhile(() => _isEntityMakeStep == false);
            currentEntity.OnStep -= OnEntityStep;

            currentEntity = _bot;
            currentEntity.OnStep += OnEntityStep;
            _isEntityMakeStep = false;
            currentEntity.GetStep();

            yield return new WaitWhile(() => _isEntityMakeStep == false);
            currentEntity.OnStep -= OnEntityStep;
        }
    }

    private void OnEntityStep(int index, EntityCellType cellType)
    {
        _fieldIntance.SetCell(index, cellType);
        _isEntityMakeStep = true;
    }
}