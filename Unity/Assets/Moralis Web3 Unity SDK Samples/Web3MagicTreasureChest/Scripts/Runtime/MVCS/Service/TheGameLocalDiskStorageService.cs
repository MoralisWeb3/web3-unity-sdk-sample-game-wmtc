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

        public async UniTask RegisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = true;
			Debug.Log("theGameLocalDiskStorage.IsRegistered is : " + theGameLocalDiskStorage.IsRegistered);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
		}

        public async UniTask UnregisterUserAsync()
        {
			await UniTask.Delay(SimulatedDelay);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = false;
			Debug.Log("theGameLocalDiskStorage.IsRegistered is : " + theGameLocalDiskStorage.IsRegistered);
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
		}



        // Event Handlers ---------------------------------

    }

}
