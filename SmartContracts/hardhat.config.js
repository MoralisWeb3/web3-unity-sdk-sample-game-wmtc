///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
require("@nomicfoundation/hardhat-toolbox");
require("dotenv").config();


///////////////////////////////////////////////////////////
// EXPORTS
///////////////////////////////////////////////////////////
/**
 * @type import('hardhat/config').HardhatUserConfig
 */
module.exports = {
    solidity: "0.8.9",
    networks: {
        mumbai: {
            url: process.env.MUMBAI_NETWORK_URL,
            accounts: [process.env.PRIVATE_KEY]
        }
    },
    etherscan: {
        apiKey: process.env.POLYGONSCAN_API_KEY
    }
};


///////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////
task("cct", "Clean, Compile, & Test the Greeter.sol").setAction(async () => {

    // Works!
    await hre.run("clean");
    await hre.run("compile");
    await hre.run("test");
});



