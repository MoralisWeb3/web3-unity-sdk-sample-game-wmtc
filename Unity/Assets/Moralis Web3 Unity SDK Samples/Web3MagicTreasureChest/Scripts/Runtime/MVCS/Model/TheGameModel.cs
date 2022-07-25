using System.Collections.Generic;
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


		// Fields -----------------------------------------

		
		// Initialization Methods -------------------------
		public TheGameModel()
		{
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
