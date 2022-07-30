///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
const hre = require("hardhat");
const fs = require('fs');

///////////////////////////////////////////////////////////
// MAIN
///////////////////////////////////////////////////////////
async function main()
{

  ///////////////////////////////////////////////////////////
  // DEPLOYMENT
  ///////////////////////////////////////////////////////////

  // Gold
  const Gold = await hre.ethers.getContractFactory("Gold");
  const gold = await Gold.deploy();
  await gold.deployed();

  // TheGameContract
  const TheGameContract = await hre.ethers.getContractFactory("TheGameContract");
  const theGameContract = await TheGameContract.deploy(gold.address);
  await theGameContract.deployed();


  ///////////////////////////////////////////////////////////
  // UNITY-FRIENDLY OUTPUT
  ///////////////////////////////////////////////////////////
  const abiFile = JSON.parse(fs.readFileSync('./artifacts/contracts/TheGameContract.sol/TheGameContract.json', 'utf8'));
  const address = theGameContract.address;
  const abi = JSON.stringify(abiFile.abi).replaceAll ('"','\\"',);
  console.log("\n");
  console.log("DEPLOYMENT COMPLETE: COPY TO UNITY...");
  console.log("\n");
  console.log("protected override void SetContractDetails()");
  console.log("{\n");
  console.log(" _address  = \"%s\";", address);
  console.log(" _abi      = \"%s\";", abi);
  console.log("\n}\n");
  console.log("\n");

  ///////////////////////////////////////////////////////////
  // WAIT
  ///////////////////////////////////////////////////////////
  console.log("WAIT ...");
  console.log("\n");
  await theGameContract.deployTransaction.wait(7);


  ///////////////////////////////////////////////////////////
  // VERIFY
  ///////////////////////////////////////////////////////////
  console.log("VERIFICATION ...");
  console.log("\n");
  await hre.run("verify:verify", {
    address: theGameContract.address,
    constructorArguments: [gold.address],
  });


  ///////////////////////////////////////////////////////////
  // LOG OUT DATA FOR USAGE IN UNITY
  ///////////////////////////////////////////////////////////
  console.log("VERIFICATION COMPLETE");
  console.log("\n");

}

///////////////////////////////////////////////////////////
// EXECUTE
///////////////////////////////////////////////////////////
main()
    .then(() => process.exit(0))
    .catch((error) => {
      console.error(error);
      process.exit(1);
    });
