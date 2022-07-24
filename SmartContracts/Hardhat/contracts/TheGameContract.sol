// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "hardhat/console.sol";
import "contracts/Gold.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   The proxy contact
//                                  for all other contracts
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract TheGameContract
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // User address who owns this contract instance
    address _goldContractAddress;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor(address goldContractAddress) 
    {
        _goldContractAddress = goldContractAddress;

        console.log(
            "TheGameContract.constructor() goldContractAddress = %s, ",
            goldContractAddress
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Add gold to the calling address
    //      *   Changes no contract state, so call via 
    //          RunContractFunction
    ///////////////////////////////////////////////////////////
    function getGold() public view returns (uint256 balance)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        balance = Gold(_goldContractAddress).getGold();
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Add gold to the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function addGold(uint256 amount) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        Gold(_goldContractAddress).addGold(amount);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Remove gold to the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function removeGold(uint256 amount) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        Gold(_goldContractAddress).removeGold(amount);
    }
    
}


