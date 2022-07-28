using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Sdk.DesignPatterns.Creational.Singleton.SingletonMonobehaviour;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	public enum DungeonArea
	{
		Intro,
		Authentication,
		Settings,
		DeveloperConsole,
		ViewCollection,
		Game

	}

	/// <summary>
	/// Handles the Dungeon for the game
	/// </summary>
	public class DungeonUI : SingletonMonobehaviour<DungeonUI>
	{
		// Properties -------------------------------------
		public GameObject Contents { get { return _contents; }}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		
		[SerializeField] 
		private GameObject _contents = null;

		[SerializeField]
		private List<ReferencePoint> _referencePoints = null;

		private Dictionary<DungeonArea, int> indexByDungeonArea = new Dictionary<DungeonArea, int>();

		//  Unity Methods----------------------------------
		protected void Awake()
		{
			//SharedHelper.DontDestroyOnLoad(gameObject);
		
		}

		protected void Start()
		{
			
			if (indexByDungeonArea.Count == 0)
            {
				indexByDungeonArea.Add(DungeonArea.Intro, 0);
				indexByDungeonArea.Add(DungeonArea.Authentication, 1);
				indexByDungeonArea.Add(DungeonArea.Settings, 2);
				indexByDungeonArea.Add(DungeonArea.DeveloperConsole, 3);
				indexByDungeonArea.Add(DungeonArea.ViewCollection, 4);
				indexByDungeonArea.Add(DungeonArea.Game, 5);
			}
		}


		// General Methods --------------------------------
		public async UniTask TweenTo(DungeonArea dungeonArea)
		{

			float duration = 0.5f;
			float delay = 0.25f * 1;

			int i = indexByDungeonArea[dungeonArea];

			ReferencePoint referencePoint = _referencePoints[i];

			bool isWaiting = true;
			TweenHelper.TransformDOBlendableMoveBy(_contents, _contents.transform.position,
				referencePoint.transform.position, duration, delay).onComplete = () =>
				{
					isWaiting = false;
				};

			await UniTask.WaitWhile(() => isWaiting);

		}



        // Event Handlers ---------------------------------


    }
}
