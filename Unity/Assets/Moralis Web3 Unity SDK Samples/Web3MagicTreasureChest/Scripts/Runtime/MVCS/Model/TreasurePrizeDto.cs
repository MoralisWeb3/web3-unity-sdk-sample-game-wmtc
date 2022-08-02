
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model
{
    public class TreasurePrizeMetadata
    {
        public string Title = "";
        public int Price = -1;
    }

    /// <summary>
    /// Stores data for the game
    /// </summary>
    public class TreasurePrizeDto : PrizeDto
    {
        // Properties -------------------------------------
        public string Title { get { return ConvertMetadataStringToObject(Metadata).Title; } }
        public int Price { get { return ConvertMetadataStringToObject(Metadata).Price; } }

        // Fields -----------------------------------------


        // Initialization Methods -------------------------
        public TreasurePrizeDto (string ownerAddress, string metadata) : base(ownerAddress, metadata) 
        {

        }

        // General Methods --------------------------------

        /// <summary>
        /// Add custom parsing outside of class hierarchy (thus, use static)
        /// </summary>
        public static string ConvertMetadataObjectToString (TreasurePrizeMetadata treasurePrizeMetadata)
        {
            return $"Title={treasurePrizeMetadata.Title}|Price={treasurePrizeMetadata.Price}";
        }

        /// <summary>
        /// Add custom parsing outside of class hierarchy (thus, use static)
        /// </summary>
        public static TreasurePrizeMetadata ConvertMetadataStringToObject(string metadata)
        {
            List<string> tokens = metadata.Split("|").ToList();
            string title = tokens[0].Split("=")[1];
            int price = int.Parse(tokens[1].Split("=")[1]);

            if (title.Length ==0 || price == 0)
            {
                throw new ArgumentException();
            }

            return new TreasurePrizeMetadata
            {
                Title = title,
                Price = price
            };
        }


        // Event Handlers ---------------------------------

    }
}
