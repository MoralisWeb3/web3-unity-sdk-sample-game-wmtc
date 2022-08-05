
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types
{
	/// <summary>
	/// Wrapper class for a Web3API Eth Contract.
	/// </summary>
	public class TheGameContract : Contract
	{

		// Properties -------------------------------------
		public override ChainList ChainList { get { return ChainList.cronos_testnet; } }
		public string TreasurePrizeContractAddress { get { return _treasurePrizeContractAddress; }
}

		// Fields -----------------------------------------
		private const bool IsLogging = true;
		private string _treasurePrizeContractAddress = "";


		// Initialization Methods -------------------------

		protected override void SetContractDetails()
	   {

	     _treasurePrizeContractAddress  = "0xd070878864a0edCA0bb6521efD7eB7b9Be98dBad";
	     _address  = "0x9A34e19d7aE9Ae6A81c0991C1A2187C16b3693bc";
	     _abi      = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"goldContractAddress\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"treasurePrizeContractAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"tokenURI\",\"type\":\"string\"}],\"name\":\"addTreasurePrize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"components\":[{\"internalType\":\"string\",\"name\":\"Title\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"Type\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"Price\",\"type\":\"uint256\"}],\"internalType\":\"struct TheGameContract.Reward\",\"name\":\"reward\",\"type\":\"tuple\"}],\"name\":\"convertRewardToString\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"rewardString\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"}],\"name\":\"deleteAllTreasurePrizes\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"addressIn\",\"type\":\"address\"}],\"name\":\"getAddress\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"addressOut\",\"type\":\"address\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getGold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"balance\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getIsRegistered\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"isRegistered\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getLastRegisteredAddress\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"lastRegisteredAddress\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"messageIn\",\"type\":\"string\"}],\"name\":\"getMessage\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"messageOut\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getRewardsHistory\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"rewardString\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"min\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"max\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"nonce\",\"type\":\"uint256\"}],\"name\":\"randomRange\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"register\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"}],\"name\":\"safeReregisterAndDeleteAllTreasurePrizes\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"sellTreasurePrize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"targetBalance\",\"type\":\"uint256\"}],\"name\":\"setGold\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"int256\",\"name\":\"delta\",\"type\":\"int256\"}],\"name\":\"setGoldBy\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"goldAmount\",\"type\":\"uint256\"}],\"name\":\"startGameAndGiveRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"unregister\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";


	   }



		/// <summary>
		/// Format for ABI:
		///		*  ExecuteContractFunction - requires string
		///		*  RunContractFunction - requires object[]. This must be manually created from the string below
		/// </summary>
		/// <returns></returns>
		protected override object[] GetAbiObject()
        {
			ContractAbi contractAbi = new ContractAbi();

			//NOTE: Its ONLY required here to manually recreate the methods
			//		called by RunContractFunction

			List<object> cInputParams = new List<object>();
			cInputParams.Add(new { internalType = "address", name = "goldContractAddress", type = "address" });
			contractAbi.AddConstructor(cInputParams);
			
			// getMessage
			List<object> testInput = new List<object>();
			testInput.Add(new { internalType = "address", name = "addressIn", type = "address" });
			List<object> testOutput = new List<object>();
			testOutput.Add(new { internalType = "address", name = "addressOut", type = "address" });
			contractAbi.AddFunction("getAddress", "pure", testInput, testOutput);
			

			// isRegistered
			List<object> isRegistered_Input = new List<object>();
			List<object> isRegistered_Output = new List<object>();
			isRegistered_Output.Add(new { internalType = "bool", name = "isRegistered", type = "bool" });
			contractAbi.AddFunction("getIsRegistered", "view", isRegistered_Input, isRegistered_Output);

			// getGold
			List<object> getGold_Input = new List<object>();
			List<object> getGold_Output = new List<object>();
			getGold_Output.Add(new { internalType = "uint256", name = "balance", type = "uint256" });
			contractAbi.AddFunction("getGold", "view", getGold_Input, getGold_Output);

			// getMsgSender
			List<object> getMsgSender_Input = new List<object>();
			List<object> getMsgSender_Output = new List<object>();
			getMsgSender_Output.Add(new { internalType = "string", name = "lastRegisteredAddress", type = "string" });
			contractAbi.AddFunction("getLastRegisteredAddress", "view", getMsgSender_Input, getMsgSender_Output);


			// getRewardsHistory
			List<object> getRewardsHistory_Input = new List<object>();
			List<object> getRewardsHistory_Output = new List<object>();
			getRewardsHistory_Output.Add(new { internalType = "string", name = "rewardTitle", type = "string" });
			getRewardsHistory_Output.Add(new { internalType = "uint256", name = "rewardType", type = "uint256" });
			getRewardsHistory_Output.Add(new { internalType = "uint256", name = "rewardPrice", type = "uint256" });
			contractAbi.AddFunction("getRewardsHistory", "view", getRewardsHistory_Input, getRewardsHistory_Output);

			return contractAbi.ToObjectArray();
		}

		///////////////////////////////////////////////////////////
		// RunContractFunctionAsync
		///////////////////////////////////////////////////////////

		// Debugging Methods --------------------------------
		public async UniTask<string> getLastRegisteredAddress()
		{
			Dictionary<string, object> args = new Dictionary<string, object>();

			string result = await RunContractFunctionAsync("getLastRegisteredAddress", args, IsLogging);
			return result;
		}
		
		
		// General Methods --------------------------------
		//TODO: Change all names to async NOT IN THIS CLASS, BUT IN ALL OTHER CLASSES
		
		
		
		public async UniTask<bool> getIsRegistered()
		{

			MoralisUser moralisUser = await TheGameSingleton.Instance.GetMoralisUserAsync();
			
			//TEMP TEST OF getAddress
			Dictionary<string, object> args = new Dictionary<string, object>();
			args.Add("addressIn", moralisUser.ethAddress);
			string result = await RunContractFunctionAsync("getAddress", args, IsLogging); //getIsRegistered

			bool resultBool = false; // bool.Parse(result);
			return resultBool;
		}

		
		public async UniTask<int> getGold()
		{
			Dictionary<string, object> args = new Dictionary<string, object>();

			string goldString = await RunContractFunctionAsync("getGold", args, IsLogging);
			int goldInt = Int32.Parse(goldString);
			return goldInt;
		}


		public async UniTask<string> GetRewardsHistory()
		{
			Dictionary<string, object> args = new Dictionary<string, object>();

			var result = await RunContractFunctionAsync("getRewardsHistory", args, IsLogging);
			
			Debug.Log("getRewardsHistory: " + result);
			Debug.LogWarning($"Must manually create a reward here from {result}");
			Reward reward = new Reward
			{
				Title = TheGameHelper.CreateNewRewardTitle(TheGameHelper.RewardGold),
				Type = TheGameHelper.GetRewardType(TheGameHelper.RewardPrize),
				Price = 13,
			};
			return reward.ToString();
		}


		///////////////////////////////////////////////////////////
		// ExecuteContractFunctionAsync
		///////////////////////////////////////////////////////////
		public async UniTask<string> Register()
		{
			object[] args =
			{
			};

			string result = await ExecuteContractFunctionAsync("register", args, IsLogging);

			return result;
		}

		
		public async UniTask<string> Unregister()
		{
			object[] args =
			{
			};

			string result = await ExecuteContractFunctionAsync("unregister", args, IsLogging);
			return result;
		}


		public async UniTask<string> setGold(int targetBalance2)
		{
			int targetBalance = targetBalance2;
			object[] args =
			{
				targetBalance
			};

			string result = await ExecuteContractFunctionAsync("setGold", args, IsLogging);

			return result;
		}

		
		public async UniTask<string> setGoldBy(int deltaBalance)
		{
			int delta = deltaBalance;
			object[] args =
			{
				delta
			};

			string result = await ExecuteContractFunctionAsync("setGoldBy", args, IsLogging);

			return result;
		}


		public async UniTask<string> AddTreasurePrize (TreasurePrizeDto treasurePrizeDto)
		{
			string metadata = treasurePrizeDto.Metadata;
			object[] args =
			{
				metadata
			};
			
			string result = await ExecuteContractFunctionAsync("addTreasurePrize", args, IsLogging);
			return result;
		}
		

		public async UniTask<string> SellTreasurePrize(TreasurePrizeDto treasurePrizeDto)
		{
			int tokenId = treasurePrizeDto.TokenId;
			
			if (tokenId == TreasurePrizeDto.NullTokenId)
			{
				Debug.Log("BurnNftAsync() failed. tokenId must be NOT null. " +
				          "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
				return "";
			}
				
			object[] args =
			{
				tokenId
			};
			
			const bool isLogging = true;
			return await ExecuteContractFunctionAsync("sellTreasurePrize", args, isLogging);
		}

		
		private int[] GetTokenIds(List<TreasurePrizeDto> treasurePrizeDtos)
		{
			int[] tokenIds = new int[treasurePrizeDtos.Count];
			for (int i = 0; i < treasurePrizeDtos.Count; i++)
			{
				int tokenId = treasurePrizeDtos[i].TokenId;

				if (tokenId == TreasurePrizeDto.NullTokenId)
				{
					throw new Exception("GetTokenIds() failed. tokenId must be NOT null. " +
					          "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
				}

				tokenIds[i] = tokenId;
			}

			return tokenIds;
		}
		
		
		public async Task<string> DeleteAllTreasurePrizes(List<TreasurePrizeDto> treasurePrizeDtos)
		{
			int[] tokenIds = GetTokenIds(treasurePrizeDtos);
			object[] args =
			{
				tokenIds
			};

			const bool isLogging = true;
			string result = await ExecuteContractFunctionAsync("deleteAllTreasurePrizes", args, isLogging);
			return result;
		}
		
		
		public async Task<string> SafeReregisterAndDeleteAllTreasurePrizes(List<TreasurePrizeDto> treasurePrizeDtos)
		{
			int[] tokenIds = GetTokenIds(treasurePrizeDtos);
			object[] args =
			{
				tokenIds
			};

			const bool isLogging = true;
			string result = await ExecuteContractFunctionAsync("safeReregisterAndDeleteAllTreasurePrizes", args, isLogging);
			return result;
		}


		public async Task<string> StartGameAndGiveRewards(int goldAmount)
		{
			object[] args =
			{
				goldAmount
			};

			string result = await ExecuteContractFunctionAsync("startGameAndGiveRewards", args, IsLogging);
			return result;
		}

		
		// Event Handlers ---------------------------------
		
	}
}
