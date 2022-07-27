using System.Collections.Generic;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model
{
	/// <summary>
	/// Stores data for the game
	///		* See <see cref="TheGameSingleton"/>
	/// </summary>
	public class TheGameModel 
	{
		// Properties -------------------------------------
		public TheGameConfiguration TheGameConfiguration { get { return TheGameConfiguration.Instance; }  }
		public Observable<int> Gold { get { return _gold; } }
		public Observable<List<TreasurePrizeDto>> TreasurePrizeDtos { get { return _treasurePrizeDtos; } }

		// Fields -----------------------------------------
		private Observable<int> _gold = new Observable<int>();
		private ObservableTreasurePrizeDtos _treasurePrizeDtos = new ObservableTreasurePrizeDtos();

		// Initialization Methods -------------------------
		public TheGameModel()
		{
			_gold.Value = 10;

			_treasurePrizeDtos.Value.Add(new TreasurePrizeDto("prize 1"));
			_treasurePrizeDtos.Value.Add(new TreasurePrizeDto("prize 2"));
			_treasurePrizeDtos.Value.Add(new TreasurePrizeDto("prize 55"));
		}

		
		// General Methods --------------------------------
		public bool HasAnyData()
		{
			return false;
		}
		
		
		public void ResetAllData()
		{
		}
		
		
		public void AddPropertyData(PropertyData propertyData)
		{
		}
		
		public void RemovePropertyData(PropertyData propertyData)
		{
		}
		
		// Event Handlers ---------------------------------


	}
}
