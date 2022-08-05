using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Attributes;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

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
		public PendingMessage PendingMessageActive { get { return _pendingMessageActive; }}
		public PendingMessage PendingMessagePassive { get { return _pendingMessagePassive; }}
		public PendingMessage PendingMessageExtraDelay { get { return _pendingMessageExtraDelay; }}
		public bool HasExtraDelay { get { return false; }}
		
		// Fields -----------------------------------------
		private readonly PendingMessage _pendingMessageActive = new PendingMessage("Loading ...", 500);
		private readonly PendingMessage _pendingMessagePassive = new PendingMessage("Loading ...", 500);
		private readonly PendingMessage _pendingMessageExtraDelay = new PendingMessage("Waiting ...", 0);

		// While LocalDiskStorage is FAST, add some delays to test the UI "Loading..." text, etc...
		private static readonly int DelaySimulatedPerMethod = 100;
		private static readonly int DelayExtraSimulatedAfterStateChange = 500;
		
		//
		private Reward _lastReward;
		
		
		// Initialization Methods -------------------------
		public TheGameLocalDiskStorageService()
		{

		}
		
		// DELAY Methods -------------------------
		public UniTask DoExtraDelayAsync()
		{
			return UniTask.Delay(DelayExtraSimulatedAfterStateChange);
		}
		
		//  GETTER - LocalDiskStorage Methods --------------------------------
		public UniTask<Reward> GetRewardsHistoryAsync()
		{
			return new UniTask<Reward>(_lastReward); 
		}

		public async UniTask<string> GetLastRegisteredAddress()
		{
			await UniTask.Delay(DelaySimulatedPerMethod);
			return "Test from LocalDiskStorage";
		}

		public async UniTask<bool> IsRegisteredAsync()
		{
			await UniTask.Delay(DelaySimulatedPerMethod);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			return theGameLocalDiskStorage.IsRegistered;
		}
		
		public async UniTask<int> GetGoldAsync()
		{
			await UniTask.Delay(DelaySimulatedPerMethod);
	        
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
	        
			return theGameLocalDiskStorage.Gold;
		}
		
		public async UniTask<List<TreasurePrizeDto>> GetTreasurePrizesAsync()
		{
			await UniTask.Delay(DelaySimulatedPerMethod);

			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

			return theGameLocalDiskStorage.TreasurePrizeDtos;
		}

		//  SETTER - LocalDiskStorage Methods --------------------------------
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
				Debug.LogWarning("Creating 'TheGameLocalDiskStorage'");
			}
			return theGameLocalDiskStorage;
		}

		
		private static bool SaveTheGameLocalDiskStorage(TheGameLocalDiskStorage theGameLocalDiskStorage)
		{
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			bool isSuccess = LocalDiskStorage.Instance.Save<TheGameLocalDiskStorage>(theGameLocalDiskStorage);
			return isSuccess;
		}

		
		private static bool ClearTheGameLocalDiskStorage()
		{
			///////////////////////////////////////////
			// Execute: Save
			///////////////////////////////////////////
			TheGameLocalDiskStorage theGameLocalDiskStorage = new TheGameLocalDiskStorage();
			bool isSuccess = LocalDiskStorage.Instance.Save<TheGameLocalDiskStorage>(theGameLocalDiskStorage);
			return isSuccess;
		}

		//  General Methods --------------------------------
		
        public async UniTask RegisterAsync()
        {
			await UniTask.Delay(DelaySimulatedPerMethod);

			ClearTheGameLocalDiskStorage();
			
			await SetGoldAsync(100);
			
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = true;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
			
		}
        

        public async UniTask UnregisterAsync()
        {
			await UniTask.Delay(DelaySimulatedPerMethod);

			ClearTheGameLocalDiskStorage();
			
			await SetGoldAsync(0);
			
			TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
			theGameLocalDiskStorage.IsRegistered = false;
			SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);

		}
        

        public async UniTask SetGoldAsync(int targetBalance)
        {
	        await UniTask.Delay(DelaySimulatedPerMethod);
	        
	        TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
	        theGameLocalDiskStorage.Gold = targetBalance;
	        SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
        }

        
        public async UniTask SetGoldByAsync(int deltaBalance)
        {
	        await UniTask.Delay(DelaySimulatedPerMethod);
	        
	        TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();
	        theGameLocalDiskStorage.Gold = theGameLocalDiskStorage.Gold + deltaBalance;
	        SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
        }


        public async UniTask AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
        {
	        await UniTask.Delay(DelaySimulatedPerMethod);
	        
	        TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

	        //TODO: Check if list contains?
	        theGameLocalDiskStorage.TreasurePrizeDtos.Add(treasurePrizeDto);
	        SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
        }

        
        public async UniTask SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeToDelete)
        {
	        await UniTask.Delay(DelaySimulatedPerMethod);

	        TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

	        int countBefore = theGameLocalDiskStorage.TreasurePrizeDtos.Count;

	        int index = theGameLocalDiskStorage.TreasurePrizeDtos.FindIndex((next) =>
	        {
		        return next.Title == treasurePrizeToDelete.Title &&
		               next.Price == treasurePrizeToDelete.Price;
	        });

	        if (index != -1)
	        {
		        theGameLocalDiskStorage.TreasurePrizeDtos.RemoveAt(index);
		        SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
		        
		        //Give gold
		        int gold = (int)treasurePrizeToDelete.Price;
		        Debug.Log($"Paying {gold} per Treasure sold");
		        await SetGoldByAsync(gold);
	        }
	        
	        

        }

        public async UniTask DeleteAllTreasurePrizeAsync()
        {
	        await UniTask.Delay(DelaySimulatedPerMethod);

	        TheGameLocalDiskStorage theGameLocalDiskStorage = LoadTheGameLocalDiskStorage();

	        theGameLocalDiskStorage.TreasurePrizeDtos.Clear();
	        SaveTheGameLocalDiskStorage(theGameLocalDiskStorage);
        }

        public async UniTask StartGameAndGiveRewardsAsync(int goldAmount)
        {
	        if (goldAmount <= 0)
	        {
		        throw new Exception("goldAmount must be > 0 to start the game");
	        }
	        
	        if (await GetGoldAsync() < goldAmount)
	        {
		        throw new Exception("getGold() must be >= goldAmount to start the game");
	        }
	        if (await IsRegisteredAsync() == false)
	        {
		        throw new Exception("Must be registered to start the game.");
	        }

	        // Deduct gold
	        await SetGoldByAsync(-goldAmount);

	        uint random = (uint)UnityEngine.Random.Range(0, 100);
	        uint price = random;
	        uint theType = 0;
	        string title = "";

	        if (random < 50)
	        {
		        // REWARD: Gold!
		        theType = TheGameHelper.GetRewardType(TheGameHelper.RewardGold);
		        title = TheGameHelper.CreateNewRewardTitle(TheGameHelper.RewardGold);
		        await SetGoldByAsync((int)price);
	        } 
	        else 
	        {
		        // REWARD: Prize!
		        theType = TheGameHelper.GetRewardType(TheGameHelper.RewardPrize);
		        title = TheGameHelper.CreateNewRewardTitle(TheGameHelper.RewardPrize);

		        //NOTE: Metadata structure must match in both: TheGameContract.sol and TreasurePrizeDto.cs
		        MoralisUser moralisUser = await TheGameSingleton.Instance.GetMoralisUserAsync();
		        
		        // RELATES ONLY TO NFT
		        TreasurePrizeMetadata treasurePrizeMetadata = new TreasurePrizeMetadata
		        {
			        Title = title,
			        Price = price
		        };
		        string metadata = TreasurePrizeDto.ConvertMetadataObjectToString(treasurePrizeMetadata);
		        TreasurePrizeDto treasurePrizeDto = new TreasurePrizeDto(moralisUser.ethAddress, metadata);
		        
		        await AddTreasurePrizeAsync(treasurePrizeDto);
	        }
	        
	        // RELATES TO NFT OR GOLD
	        _lastReward = new Reward
	        {
		        Title = title,
		        Type = theType,
		        Price = price
	        };
        }

        
        // Event Handlers ---------------------------------
    }
}
