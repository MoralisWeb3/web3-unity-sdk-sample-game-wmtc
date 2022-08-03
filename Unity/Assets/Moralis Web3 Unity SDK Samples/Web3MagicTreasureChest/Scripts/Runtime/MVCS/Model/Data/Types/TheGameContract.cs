
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types
{
	public class MessageHolder
    {
		public string message = "";
    }

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

			_treasurePrizeContractAddress = "0x82e24dA9e616396443b1ad4f2a516e8F70025731";
			_address = "0x85E5B154E94B49da113E62196258832b56Dac0e5";
			_abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"goldContractAddress\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"treasurePrizeContractAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"burnNft\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256[]\",\"name\":\"tokenIds\",\"type\":\"uint256[]\"}],\"name\":\"burnNfts\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getGold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"balance\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getMsgSender\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"msgSender\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getRewardsHistory\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"rewardTitle\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"rewardType\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"rewardPrice\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"isRegistered\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"isPlayerRegistered\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"tokenURI\",\"type\":\"string\"}],\"name\":\"mintNft\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"min\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"max\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"nonce\",\"type\":\"uint256\"}],\"name\":\"randomRange\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"register\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"targetBalance\",\"type\":\"uint256\"}],\"name\":\"setGold\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"int256\",\"name\":\"delta\",\"type\":\"int256\"}],\"name\":\"setGoldBy\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"goldAmount\",\"type\":\"uint256\"}],\"name\":\"startGameAndGiveRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"unregister\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

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

			// isRegistered
			List<object> isRegistered_Input = new List<object>();
			List<object> isRegistered_Output = new List<object>();
			isRegistered_Output.Add(new { internalType = "bool", name = "isPlayerRegistered", type = "bool" });
			contractAbi.AddFunction("isRegistered", "view", isRegistered_Input, isRegistered_Output);

			// getGold
			List<object> getGold_Input = new List<object>();
			List<object> getGold_Output = new List<object>();
			getGold_Output.Add(new { internalType = "uint256", name = "balance", type = "uint256" });
			contractAbi.AddFunction("getGold", "view", getGold_Input, getGold_Output);

			// getMsgSender
			List<object> getMsgSender_Input = new List<object>();
			List<object> getMsgSender_Output = new List<object>();
			getMsgSender_Output.Add(new { internalType = "string", name = "msgSender", type = "string" });
			contractAbi.AddFunction("getMsgSender", "view", getMsgSender_Input, getMsgSender_Output);


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


		// General Methods --------------------------------
		public async UniTask<bool> isRegistered()
		{
			object[] args =
			{
			};

			//TODO: This always returns false. its my first mapping. Problem? 
			string result = await RunContractFunctionAsync("isRegistered", args, IsLogging);

			bool resultBool = bool.Parse(result);
			Debug.Log("resultBool: " + resultBool);
			return resultBool;
		}

		public async UniTask<int> getGold()
		{
			object[] args =
			{
			};

			string goldString = await RunContractFunctionAsync("getGold", args, IsLogging);
			int goldInt = Int32.Parse(goldString);
			return goldInt;
		}

		public async UniTask<string> getMsgSender()
		{
			object[] args =
			{
			};

			string result = await RunContractFunctionAsync("getMsgSender", args, IsLogging);
			return result;
		}

		public async UniTask<Reward> GetRewardsHistory()
		{
			object[] args =
			{
			};

			string result = await RunContractFunctionAsync("getRewardsHistory", args, IsLogging);
			
			Debug.LogWarning($"Must manually create a reward here from {result}");
			Reward reward = new Reward
			{
				Title = "blah",
				Type = 1,
				Price = 10,
			};
			return reward;
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


		public async UniTask<string> MintNftAsync (TreasurePrizeDto treasurePrizeDto)
		{
			string metadata = treasurePrizeDto.Metadata;
			object[] args =
			{
				metadata
			};
			
			string result = await ExecuteContractFunctionAsync("mintNft", args, IsLogging);
			return result;
		}
		

		public async UniTask<string> BurnNftAsync(TreasurePrizeDto treasurePrizeDto)
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
			return await ExecuteContractFunctionAsync("burnNft", args, isLogging);
		}


		public async Task<string> BurnNftsAsync(List<TreasurePrizeDto> treasurePrizeDtos)
		{
			int[] tokenIds = new int[treasurePrizeDtos.Count];
			for (int i = 0; i < treasurePrizeDtos.Count; i++)
			{
				int tokenId = treasurePrizeDtos[i].TokenId;

				if (tokenId == TreasurePrizeDto.NullTokenId)
				{
					Debug.Log("BurnNftsAsync() failed. tokenId must be NOT null. " +
							  "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
					return "";
				}

				tokenIds[i] = tokenId;
			}

			object[] args =
			{
				tokenIds
			};

			const bool isLogging = true;
			string result = await ExecuteContractFunctionAsync("burnNfts", args, isLogging);
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
