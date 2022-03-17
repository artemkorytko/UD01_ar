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
   
   private Field _fieldInstance;
   private FieldPositionController _fieldPositionController;
   private FieldSettings _fieldSettings;

   private Coroutine _gameCoroutine;


   private void Awake()
   {
      _player = GetComponent<Player>();
      _bot = GetComponent<Bot>();
      
      _fieldInstance = Instantiate(fieldPrefab).GetComponent<Field>();
      _fieldInstance.Initialize();
      _fieldInstance.Refresh();
      _fieldInstance.OnCompleted += OnCompleted;

      _fieldSettings = _fieldInstance.GetComponent<FieldSettings>();

      _fieldPositionController = _fieldInstance.GetComponent<FieldPositionController>();
      _fieldPositionController.Initialize(arRaycastManager,arPlaneManager,arAnchorManager);
      _fieldPositionController.IsActive = true;
      
      
   }

   private void Start()
   {
      StartGame();
   }

   private void OnCompleted(EntityCellType obj)
   {
     
   }

   public void StartGame()
   {
      _gameCoroutine = StartCoroutine(GameCoroutine());
   }

   private IEnumerator GameCoroutine()
   {
      while (true)
      {
         Entity currentEntity = _player;
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
     _fieldInstance.SetCell(index,cellType);
     _isEntityMakeStep = true;
   }
}
