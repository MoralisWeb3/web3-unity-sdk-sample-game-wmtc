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
		public List<PropertyData> PropertyDatas { get { return _propertyDatas; } set { _propertyDatas = value; } }

		
		// Fields -----------------------------------------
		private List<PropertyData> _propertyDatas = new List<PropertyData>();

		
		// Initialization Methods -------------------------
		public TheGameModel()
		{
		}

		
		// General Methods --------------------------------
		public bool HasAnyData()
		{
			return PropertyDatas.Count > 0;
		}
		
		
		public void ResetAllData()
		{
			_propertyDatas.Clear();
		}
		
		
		public void AddPropertyData(PropertyData propertyData)
		{
			PropertyDatas.Add(propertyData);
		}
		
		public void RemovePropertyData(PropertyData propertyData)
		{
			PropertyDatas.Remove(propertyData);
		}
		
		// Event Handlers ---------------------------------


	}
}
