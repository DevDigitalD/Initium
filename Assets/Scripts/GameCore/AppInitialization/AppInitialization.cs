using System;
using System.Collections;
using System.Collections.Generic;
using ManagersCore;
using ManagersCore.IoC;
using ManagersCore.MessageSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameCore.AppInitialization
{
    public class AppInitialization : IDisposable
    {
        private readonly GameObject _appGo;
        private readonly MessagingManager _messenger;
        private readonly List<IManager> _managersList;
        private const bool DEBUG_MODE = true;

        private ManagerRegistrar _managerRegistrar;
        private IList<InitializationStepBase> _initializationSteps;
        private int _currentStepIndex;

        public AppInitialization(GameObject appGo)
        {
            _appGo = appGo;
            _managersList = new List<IManager>();
            
            InitializeManagers();
            _messenger = UnityContainer.Resolve<MessagingManager>();
        }

        private void InitializeManagers()
        {
            _managerRegistrar = new ManagerRegistrar();
            
            foreach (KeyValuePair<Type, Type> managerTypes in _managerRegistrar.ManagersType)
            {
                Type implementationType = managerTypes.Key;
                Type bindType = managerTypes.Value;
                CreateAndBindManagerImplementation(implementationType, bindType);
            }
        }

        private void CreateAndBindManagerImplementation(Type implementation, Type bindAs)
        {
            Assert.IsNotNull(implementation);
            Assert.IsNotNull(bindAs);
            
            if (!bindAs.IsAssignableFrom(implementation))
                Debug.LogError($"AppInitialization: Cannot properly bind implementation {implementation} as {bindAs}." +
                               $"Implementation must be derived from binding type or equals {bindAs}.");
            
            IManager instance;
                
            if (implementation.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var gameObject = new GameObject(implementation.Name);
                gameObject.transform.SetParent(_appGo.transform);
                Component component = gameObject.AddComponent(implementation);
                instance = component as IManager;
            }
            else
            {
                instance = Activator.CreateInstance(implementation) as IManager;
            }

            UnityContainer.Bind(instance, bindAs, true);
            _managersList.Add(instance);
        }

        public void InitSystems()
        {
            if (DEBUG_MODE)
                Debug.Log("Initiating systems " + Time.time);

            AddListeners();
            
            foreach (IManager manager in _managersList)
            {
                manager.Init();
            }

            // data
            // auth
            // configs
            // assets
            // menu
            _initializationSteps = new List<InitializationStepBase>
            {
                // new LoadLocalDataStep(this),
                // /*new LoadPlayFabPlayerProfileStep(this),
                // new RegisterForPushStep(this),*/
                // new LoadConfigsStep(this),
                // new CheckChaptersStep(this)
            };

            NextStep();
        }

        private void AddListeners()
        {
            // _messenger.Data.AddListener(DataActions.OnSavedDataLoaded, OnDataLoaded);
            //
            // _messenger.Network.AddListener(NetworkActions.OnAuthenticationComplete, OnAuthFinished);
            //
            // _messenger.Data.AddListener(DataActions.OnDownloadedConfig, OnDownloadedConfigs);
        }

        public void Dispose()
        {
            // _messenger.Data.RemoveListener(DataActions.OnSavedDataLoaded, OnDataLoaded);
            //
            // _messenger.Network.RemoveListener(NetworkActions.OnAuthenticationComplete, OnAuthFinished);
            //
            // _messenger.Data.RemoveListener(DataActions.OnDownloadedConfig, OnDownloadedConfigs);

            if (DEBUG_MODE)
                Debug.Log("Disposing app initialization");

            GC.SuppressFinalize(this);
        }

        // void OnDataLoaded(DataMessage data)
        // {
        //     if (DEBUG_MODE)
        //         Debug.Log("Data loaded " + Time.time);
        // }

        void DownloadConfigs()
        {
            if (DEBUG_MODE)
                Debug.Log("Downloading configs " + Time.time);
        }

        // void OnAuthFinished(NetworkMessage message)
        // {
        //     if (DEBUG_MODE)
        //         Debug.Log("Auth finished " + Time.time);
        //
        //     DownloadConfigs();
        // }

        // private void OnDownloadedConfigs(DataMessage obj)
        // {
        //     if (DEBUG_MODE)
        //         Debug.Log("Configs download finished " + Time.time);
        //
        //     DownloadAssets();
        // }

        // private void DownloadAssets()
        // {
        //     // do here assets loading
        //
        //     if (DEBUG_MODE)
        //         Debug.Log("Downloading assets " + Time.time);
        //
        //     UnityContainer.Resolve<ICoroutineManager>().StartNewCoroutine(DownloadAssetsProcess());
        // }

        // private IEnumerator DownloadAssetsProcess()
        // {
        //     // downloading here assets ...
        //
        //     yield return new WaitForSeconds(1f);
        //
        //     if (DEBUG_MODE)
        //         Debug.Log("Assets downloaded " + Time.time);
        //
        //     _messenger.App.SendMessage(AppActions.OnAssetsLoaded);
        // }

        public void NextStep()
        {
            if (_currentStepIndex < _initializationSteps.Count)
            {
                _currentStepIndex++;
                Debug.Log("--- Next step : " + _initializationSteps[_currentStepIndex - 1]);
                _initializationSteps[_currentStepIndex - 1].RunStep();
            }
            else
            {
                Debug.Log("--- Next step ELSE");

                // _messenger.App.SendMessage(AppActions.OnSystemsInited);
            }
        }

        public void RepeatStep()
        {
            _initializationSteps[_currentStepIndex - 1].RunStep();
        }
    }
}