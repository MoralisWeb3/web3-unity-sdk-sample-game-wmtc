using System.Collections.Generic;
using MoralisUnity.Samples.Shared.Attributes;
using MoralisUnity.Samples.Shared.Data.Types.Configuration;
using MoralisUnity.Samples.Shared.Data.Types.Storage;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types
{
    /// <summary>
    /// Main configuration for the game. Click the instance of this class in the project to view/edit
    /// </summary>
    [ReferenceByGuid (Guid = "259c0de8152c6974e811ad9ec6e1cb58")]
    [CreateAssetMenu( menuName = TheGameConstants.PathCreateAssetMenu + "/" + Title,  fileName = Title)]
    public class TheGameConfiguration : BaseConfiguration<TheGameConfiguration>
    {
        // Properties -------------------------------------
        public TheGameView TheGameViewPrefab { get { return _theGameViewPrefab; } }
        public SceneData IntroSceneData { get { return _introSceneData;}}
        public SceneData AuthenticationSceneData { get { return _authenticationSceneData;}}
        public SceneData SettingsSceneData { get { return _settingsSceneData;}}
        public SceneData GameSceneData { get { return _gameSceneData;}}
        public TheGameServiceType TheGameServiceType { get { return _theGameServiceType;}}

        [SerializeField]
        private List<SceneData> _sceneDatas = null;

        // Fields -----------------------------------------
        public const string Title = TheGameConstants.ProjectName + " Configuration";
        
        [Header("References (Project)")]
        [SerializeField]
        private TheGameView _theGameViewPrefab = null;

        [SerializeField] 
        private SceneData _introSceneData = null;

        [SerializeField] 
        private SceneData _authenticationSceneData = null;

        [SerializeField] 
        private SceneData _settingsSceneData = null;

        [SerializeField] 
        private SceneData _gameSceneData = null;

        [Header("Settings (Edit-Time Only)")]
        
        [Tooltip("Use either Moralis Database (dev) or Moralis Web3 (prod)")]
        [SerializeField]
        public TheGameServiceType _theGameServiceType = TheGameServiceType.Null;
        
        // Unity Methods ----------------------------------

        
        // General Methods --------------------------------

		
        // Event Handlers ---------------------------------
    }
}