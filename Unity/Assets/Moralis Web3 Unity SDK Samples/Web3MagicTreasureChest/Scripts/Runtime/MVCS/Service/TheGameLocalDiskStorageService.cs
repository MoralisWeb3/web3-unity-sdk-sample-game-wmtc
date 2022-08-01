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

		public static bool ClearTheGameLocalDiskStorage()
		{
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			TheGameLocalDiskStorage theGameLocalDiskStorage = new TheGameLocalDiskStorage();
			bool isSuccess = LocalDiskStorage.Instance.Save<TheGameLocalDiskStorage>(theGameLocalDiskStorage);
			return isSuccess;
		}

		//  General Methods --------------------------------

		public async UniTask<bool> IsRegisteredAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			Debug.Log("theGameLocalDiskStorage.IsRegistered was : " + theGameLocalDiskStorage.IsRegistered);
			return theGameLocalDiskStorage.IsRegistered;
		}

        public async UniTask<bool> RegisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			ClearTheGameLocalDiskStorage();
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = true;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
			return theGameLocalDiskStorage.IsRegistered;
		}

        public async UniTask<bool> UnregisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			ClearTheGameLocalDiskStorage();
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


		public async UniTask<List<TreasurePrizeDto>> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			//TODO: Check if list contains?
			theGameLocalDiskStorage.TreasurePrizeDtos.Add(treasurePrizeDto);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.TreasurePrizeDtos;
		}

		public async UniTask<List<TreasurePrizeDto>> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			//TODO: Check if list contains?
			theGameLocalDiskStorage.TreasurePrizeDtos.Remove(treasurePrizeDto);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

			return theGameLocalDiskStorage.TreasurePrizeDtos;
		}

        public UniTask<int> GetGold(int delta)
        {
            throw new System.NotImplementedException();
        }

        UniTask<string> ITheGameService.RegisterAsync()
        {
            throw new System.NotImplementedException();
        }

        UniTask<string> ITheGameService.UnregisterAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<string> SetGoldAsync(int targetBalance)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<string> SetGoldByAsync(int deltaBalance)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<int> GetGoldAsync()
        {
            throw new System.NotImplementedException();
        }


        // Event Handlers ---------------------------------

    }

}
