// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "classes/Reward.sol";
import "@openzeppelin/contracts/utils/Strings.sol";

///////////////////////////////////////////////////////////
// LIBRARIES
///////////////////////////////////////////////////////////
library TheGameLibrary 
{
    // Defining structure
    struct Constants {

        // Declaring variables
        uint Pi;             
        uint EulerNb;        
        uint PythagoraConst; 
        uint TheodorusConst; 
    }
    

    function randomRange (uint min, uint max, uint nonce) public view returns (uint) 
    {
        // The nonce is especially useful for unit-tests, to ensure variation
        uint randomnumber = uint(keccak256(abi.encodePacked(block.timestamp, block.difficulty, msg.sender, nonce))) % (max);
        randomnumber = randomnumber + min;
        return randomnumber;
    }
    
    function convertRewardToString (Reward memory reward) public pure returns (string memory rewardString) 
    {
        string memory titleString = reward.Title;
        string memory typeString = Strings.toString(reward.Type);
        string memory priceString = Strings.toString(reward.Price);

        if (bytes(typeString).length == 0)
        {
            typeString = "0";
        }

        if (bytes(priceString).length == 0)
        {
            priceString = "0";
        }


        rewardString = string(abi.encodePacked("Title=", titleString, "|Type=", typeString, "|Price=", priceString));
    }

    //TODO: This works great. It exists only for use in testTheGameLibrary.js. Can I move it from here?
    function createNewRewardForTesting () public pure returns (Reward memory reward)
    {
        uint price = 0;
        uint theType = 0;
        string memory title = "";

        reward = Reward (
        {
            Title: title,
            Type: theType,
            Price: price
        });
    }

    //TODO: This works great. It exists only for use in testTheGameLibrary.js. Can I move it from here?
    function convertRewardToStringForTesting () public pure returns (string memory stringReward)
    {
        Reward memory reward2 = createNewRewardForTesting();
        stringReward =  convertRewardToString (reward2);
    }
}