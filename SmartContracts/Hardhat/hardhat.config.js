///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
require("@nomicfoundation/hardhat-toolbox");
require("dotenv").config();

// TODO: MOve this to env
const PRIVATE_KEY = "2f6009cddf4c79754af198995fd9db86f0c4ced09e5e33b8f0d701362f8231d5"; 

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
      url: "https://nd-316-787-845.p2pify.com/a821626ba4dc9a6bb7578106ee0615c0",
      accounts: [PRIVATE_KEY]
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