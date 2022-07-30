
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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


		// Fields -----------------------------------------
		private const bool IsLogging = true;


		// Initialization Methods -------------------------
		protected override void SetContractDetails()
		{

			_address = "0xD60fa6C3083d3df7Be7D9C58d1a4b1Fe97AE5066";
			_abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"goldContractAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"balance\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getTest\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"message\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"}]";

		}

		/// <summary>
		/// Format for ABI:
		///		*  ExecuteContractFunction - requires string
		///		*  RunContractFunction - requires object[]. This must be manually created from the string
		/// </summary>
		/// <returns></returns>
		protected override object[] GetAbiObject()
		{
			object[] newAbi = new object[3];

			// constructor
			object[] cInputParams = new object[1];
			cInputParams[0] = new { internalType = "address", name = "goldContractAddress", type = "address" };
			newAbi[0] = new { inputs = cInputParams, name = "", stateMutability = "nonpayable", type = "constructor" };

			// getGold
			object[] ggInputParams = new object[0];
			object[] ggOutputParams = new object[1];
			ggOutputParams[0] = new { internalType = "uint256", name = "balance", type = "uint256" };
			newAbi[1] = new { inputs = ggInputParams, outputs = ggOutputParams, name = "getGold", stateMutability = "view", type = "function" };

			// getTest
			object[] gtInputParams = new object[0];
			object[] gtOutputParams = new object[1];
			gtOutputParams[0] = new { internalType = "string", name = "message", type = "string" };
			newAbi[1] = new { inputs = gtInputParams, outputs = gtOutputParams, name = "getTest", stateMutability = "pure", type = "function" };


			return newAbi;
		}


		// General Methods --------------------------------
		public async UniTask<string> getGold()
		{
			
			object[] args = null;
			
			string result = await ExecuteContractFunctionAsync("getTest", args, IsLogging);

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
