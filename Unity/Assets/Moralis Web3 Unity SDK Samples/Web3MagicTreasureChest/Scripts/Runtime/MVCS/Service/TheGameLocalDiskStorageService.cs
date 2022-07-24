using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Attributes;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	[CustomFilePath(LocalDiskStorage.Title + "/SimCityWeb3LocalData.txt", CustomFilePathLocation.StreamingAssetsPath)]
	[System.Serializable]
	public class SimCityWeb3LocalData
	{
		//  Properties ------------------------------------
		
		//  Fields ----------------------------------------
		public List<PropertyData> PropertyDatas = new List<PropertyData>();
	}
	
	/// <summary>
	/// Depending on <see cref="TheGameServiceType"/> this is enabled.
	///		* Handles connection to external resource of Moralis Database
	/// </summary>
	public class TheGameLocalDiskStorageService : ITheGameService
	{
		// Properties -------------------------------------
		public PendingMessage PendingMessageForDeletion { get { return _pendingMessageForDeletion; }}
		public PendingMessage PendingMessageForSave { get { return _pendingMessageForSave; }}
		
		
		// Fields -----------------------------------------
		private readonly PendingMessage _pendingMessageForDeletion = new PendingMessage("Deleting Object From LocalDiskStorage", 500);
		private readonly PendingMessage _pendingMessageForSave = new PendingMessage("Saving Object To LocalDiskStorage", 500);
		
		
		// Initialization Methods -------------------------
		public TheGameLocalDiskStorageService()
		{

		}
		
		//  LocalDiskStorage Methods --------------------------------
		
		public static SimCityWeb3LocalData LoadSimCityWeb3LocalData()
		{
			bool hasSimCityWeb3LocalData = LocalDiskStorage.Instance.Has<SimCityWeb3LocalData>();

			SimCityWeb3LocalData simCityWeb3LocalData = null;
			if (hasSimCityWeb3LocalData)
			{
				///////////////////////////////////////////
				// Execute: Load
				///////////////////////////////////////////
				simCityWeb3LocalData = LocalDiskStorage.Instance.Load<SimCityWeb3LocalData>();
			}
			else
			{
				///////////////////////////////////////////
				// Execute: Create
				///////////////////////////////////////////
				simCityWeb3LocalData = new SimCityWeb3LocalData();
				Debug.LogWarning("create new data");
			}
			return simCityWeb3LocalData;
		}
		
		public static bool SaveSimCityWeb3LocalData(SimCityWeb3LocalData simCityWeb3LocalData)
		{
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			bool isSuccess = LocalDiskStorage.Instance.Save<SimCityWeb3LocalData>(simCityWeb3LocalData);
			return isSuccess;
		}
		
		
		// General Methods --------------------------------
		public async UniTask<List<PropertyData>> LoadPropertyDatasAsync()
		{
			SimCityWeb3LocalData simCityWeb3LocalData = LoadSimCityWeb3LocalData();
			return simCityWeb3LocalData.PropertyDatas;
		}

		
		public async UniTask<PropertyData> SavePropertyDataAsync(PropertyData propertyData)
		{
			SimCityWeb3LocalData simCityWeb3LocalData = LoadSimCityWeb3LocalData();

			int foundIndex = simCityWeb3LocalData.PropertyDatas.FindIndex( propertyData1 => AreSamePropertyData(propertyData1, propertyData));

			if (foundIndex == -1)
			{
				///////////////////////////////////////////
				// Execute: Add
				///////////////////////////////////////////
				simCityWeb3LocalData.PropertyDatas.Add(propertyData);
				
				///////////////////////////////////////////
				// Execute: Save
				///////////////////////////////////////////
				SaveSimCityWeb3LocalData(simCityWeb3LocalData);
			}
			else
			{
				Debug.LogError("This already exists. Data is corrupt?");
			}
			
			// Return the original, untouched
			// The method signature is more helpful for the
			// ContractService which adds the token address
			return propertyData;
		}
		
		
		public async UniTask DeletePropertyDataAsync(PropertyData propertyData)
		{
			SimCityWeb3LocalData simCityWeb3LocalData = LoadSimCityWeb3LocalData();

			int foundIndex = simCityWeb3LocalData.PropertyDatas.FindIndex( propertyData1 => AreSamePropertyData(propertyData1, propertyData));
			
			if (foundIndex != -1)
			{
				///////////////////////////////////////////
				// Execute: Remove
				///////////////////////////////////////////
				simCityWeb3LocalData.PropertyDatas.RemoveAt(foundIndex);
				
				///////////////////////////////////////////
				// Execute: Save
				///////////////////////////////////////////
				SaveSimCityWeb3LocalData(simCityWeb3LocalData);
			}
			else
			{
				Debug.LogError("Was not found. Choose new C# to find this.");
			}
		}

		
		public async UniTask DeleteAllPropertyDatasAsync(List<PropertyData> propertyDatas)
		{
			SimCityWeb3LocalData simCityWeb3LocalData = LoadSimCityWeb3LocalData();

			///////////////////////////////////////////
			// Execute: Remove
			///////////////////////////////////////////
			simCityWeb3LocalData.PropertyDatas.Clear();
				
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			SaveSimCityWeb3LocalData(simCityWeb3LocalData);
		}
		
		
		private bool AreSamePropertyData(PropertyData a, PropertyData b)
		{
			if (a.Latitude.Equals(b.Latitude) &&
			    a.Longitude.Equals(b.Longitude) &&
			    a.OwnerAddress.Equals(b.OwnerAddress) 
			   )
			{
				return true;
			}

			return false;
		}

		
		// Event Handlers ---------------------------------

	}

}
