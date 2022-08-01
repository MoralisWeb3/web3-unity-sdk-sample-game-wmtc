// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "hardhat/console.sol";
import "@openzeppelin/contracts/utils/Strings.sol";

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
    // FUNCTION: CRUD
    //      *   Get gold amount for the calling address
    //      *   Changes no contract state, so call via 
    //          RunContractFunction
    ///////////////////////////////////////////////////////////
    function getGold() public view returns (uint256 balance)
    {
        balance = balanceOf(msg.sender);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Set gold amount for the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function setGold(uint256 targetBalance) public 
    {
        uint256 oldBalance = getGold();
        int delta = int(targetBalance) - int(oldBalance);
        
        if (delta > 0)
        {
            // console.log ('delta %s POS ', uint256(delta));
            addGold (uint256(delta));
        }
        else if (delta < 0)
        {
            //console.log ('delta %s NEG ', uint256(-delta));
            removeGold (uint256(-delta));
        }
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Set gold amount for the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function setGoldBy(int deltaBalance) public
    {
        if (deltaBalance > 0)
        {
            addGold (uint256(deltaBalance));
        }
        else if (deltaBalance < 0)
        {
            removeGold (uint256(-deltaBalance));
        }
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Add gold to the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function addGold(uint256 amount) private 
    {
        _mint(msg.sender, amount);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: CRUD
    //      *   Remove gold to the calling address
    //      *   Changes contract state, so call via 
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function removeGold(uint256 amount) private 
    {
        _burn(msg.sender, amount);
    }
}


