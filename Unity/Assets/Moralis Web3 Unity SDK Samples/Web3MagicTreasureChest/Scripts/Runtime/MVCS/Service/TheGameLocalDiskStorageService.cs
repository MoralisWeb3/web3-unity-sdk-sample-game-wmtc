using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Attributes;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	[CustomFilePath(LocalDiskStorage.Title + "/TheGameLocalDiskStorage.txt", CustomFilePathLocation.StreamingAssetsPath)]
	[System.Serializable]
	public class TheGameLocalDiskStorage
	{
		//  Properties ------------------------------------

		//  Fields ----------------------------------------
		public bool IsRegistered = false;
		public int Gold = 0;
		public List<TreasurePrizeDto> TreasurePrizeDtos = new List<TreasurePrizeDto>();
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
		private const int SimulatedDelay = 100;

		// Initialization Methods -------------------------
		public TheGameLocalDiskStorageService()
		{

		}


		//  LocalDiskStorage Methods --------------------------------

		public static TheGameLocalDiskStorage LoadTheGameLocalDiskStorage()
		{
			bool hasTheGameLocalDiskStorage = LocalDiskStorage.Instance.Has<TheGameLocalDiskStorage>();

			TheGameLocalDiskStorage theGameLocalDiskStorage = null;
			if (hasTheGameLocalDiskStorage)
			{
				///////////////////////////////////////////
				// Execute: Load
				///////////////////////////////////////////
				theGameLocalDiskStorage = LocalDiskStorage.Instance.Load<TheGameLocalDiskStorage>();
			}
			else
			{
				///////////////////////////////////////////
				// Execute: Create
				///////////////////////////////////////////
				theGameLocalDiskStorage = new TheGameLocalDiskStorage();
				Debug.LogWarning("create new data");
			}
			return theGameLocalDiskStorage;
		}

		public static bool SaveTheGameLocalDiskStorage(TheGameLocalDiskStorage theGameLocalDiskStorage)
		{
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			bool isSuccess = LocalDiskStorage.Instance.Save<TheGameLocalDiskStorage>(theGameLocalDiskStorage);
			return isSuccess;
		}

		//  General Methods --------------------------------

		public async UniTask<bool> IsRegisteredUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			Debug.Log("theGameLocalDiskStorage.IsRegistered was : " + theGameLocalDiskStorage.IsRegistered);
			return theGameLocalDiskStorage.IsRegistered;
		}

        public async UniTask<bool> RegisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = true;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.IsRegistered;
		}

        public async UniTask<bool> UnregisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = false;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.IsRegistered;
		}

        public async UniTask<int> AddGold(int delta)
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.Gold += delta;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.Gold;
		}

        public async UniTask<int> SpendGold(int delta)
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.Gold -= delta;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.Gold;
		}


		public async UniTask<List<TreasurePrizeDto>> AddTreasurePrize(TreasurePrizeDto treasurePrizeDto)
		{
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			//TODO: Check if list contains?
			theGameLocalDiskStorage.TreasurePrizeDtos.Add(treasurePrizeDto);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.TreasurePrizeDtos;
		}

		public async UniTask<List<TreasurePrizeDto>> SellTreasurePrize(TreasurePrizeDto treasurePrizeDto)
		{
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			//TODO: Check if list contains?
			theGameLocalDiskStorage.TreasurePrizeDtos.Remove(treasurePrizeDto);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.TreasurePrizeDtos;
		}


		// Event Handlers ---------------------------------

	}

}
