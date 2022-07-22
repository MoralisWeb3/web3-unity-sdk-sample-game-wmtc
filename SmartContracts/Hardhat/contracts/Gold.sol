// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "hardhat/console.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   Each contract instance 
//                                  manages CRUD for its 
//                                  greeting text message
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract Gold is ERC20 
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // User address who owns this contract instance
    address _owner;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor() ERC20 ("Gold", "GOLD") 
    {
        _owner = msg.sender;

        console.log(
            "Gold.constructor() _owner = %s",
            _owner
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: SETTER
    //      *   Set greeting 
    //      *   Changes contract state, so requires calling via
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function giveGold(uint256 amount) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        _mint(msg.sender, amount);
    }
}


