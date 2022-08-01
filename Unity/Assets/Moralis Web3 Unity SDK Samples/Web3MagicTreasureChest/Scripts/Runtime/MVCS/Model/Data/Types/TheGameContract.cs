
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
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


		// Fields -----------------------------------------
		private const bool IsLogging = true;


		// Initialization Methods -------------------------
		protected override void SetContractDetails()
		{

			_address = "0x4a3ce1701895133A67b08B76E94eB15B07d88121";
			_abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"goldContractAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"balance\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"isRegistered\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"isPlayerRegistered\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"register\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"targetBalance\",\"type\":\"uint256\"}],\"name\":\"setGold\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"int256\",\"name\":\"delta\",\"type\":\"int256\"}],\"name\":\"setGoldBy\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"unregister\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

		}


		/// <summary>
		/// Format for ABI:
		///		*  ExecuteContractFunction - requires string
		///		*  RunContractFunction - requires object[]. This must be manually created from the string
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

			// getGold
			List<object> getGold_Input = new List<object>();
			List<object> getGold_Output = new List<object>();
			getGold_Output.Add(new { internalType = "uint256", name = "balance", type = "uint256" });
			contractAbi.AddFunction("getGold", "view", getGold_Input, getGold_Output);

			// isRegistered
			List<object> isRegistered_Input = new List<object>();
			List<object> isRegistered_Output = new List<object>();
			isRegistered_Output.Add(new { internalType = "bool", name = "isPlayerRegistered", type = "bool" });
			contractAbi.AddFunction("isRegistered", "view", isRegistered_Input, isRegistered_Output);

			return contractAbi.ToObjectArray();
		}


		// General Methods --------------------------------
		public async UniTask<bool> isRegistered()
		{
			object[] args =
			{
			};

			//TODO: This always returns false. its my first mapping. Problem? 
			string result = await RunContractFunctionAsync("isRegistered", args, IsLogging);

			//HACK: Use this instead. Its dependable
			int goldInt = await getGold();

			return goldInt != 0;
		}

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

		public async UniTask<int> getGold()
		{
			object[] args =
			{
			};

			string goldString = await RunContractFunctionAsync("getGold", args, IsLogging);
			int goldInt = Int32.Parse(goldString);
			return goldInt;
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


		public async UniTask<string> MintPropertyNftAsync (PropertyData propertyData)
		{
			string metadata = propertyData.GetMetadata();
			object[] args =
			{
				metadata
			};
			
			string result = await ExecuteContractFunctionAsync("mintPropertyNft", args, IsLogging);
			return result;
		}
		

		public async UniTask<string> BurnPropertyNftAsync (PropertyData propertyData)
		{
			int tokenId = propertyData.TokenId;
			
			if (tokenId == PropertyData.NullTokenAddress)
			{
				Debug.Log("BurnPropertyNftAsync() failed. tokenId must be NOT null. " +
				          "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
				return "";
			}
				
			object[] args =
			{
				tokenId
			};
			
			const bool isLogging = true;
			return await ExecuteContractFunctionAsync("burnPropertyNft", args, isLogging);
		}


		// Event Handlers ---------------------------------
		public async Task<string> BurnPropertyNftsAsync(List<PropertyData> propertyDatas)
		{
			int[] tokenIds = new int[propertyDatas.Count];
			for  (int i = 0; i < propertyDatas.Count; i++)
			{
				int tokenId = propertyDatas[i].TokenId;
			
				if (tokenId == PropertyData.NullTokenAddress)
				{
					Debug.Log("BurnPropertyNftsAsync() failed. tokenId must be NOT null. " +
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
			string result = await ExecuteContractFunctionAsync("burnPropertyNfts", args, isLogging);
			return result;
		}
	}
}
