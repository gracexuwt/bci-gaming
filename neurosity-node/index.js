const {Neurosity} = require("@neurosity/sdk");
require("dotenv").config();

const deviceId = process.env.DEVICE_ID || "";
const email = process.env.EMAIL || "";
const password = process.env.PASSWORD || "";

const verifyEnvs = (email, password, deviceId) => {
  const invalidEnv = (env) => {
    return env === "" || env === 0;
  };
  if (invalidEnv(email) || invalidEnv(password) || invalidEnv(deviceId)) {
    console.error(
      "Please verify deviceId, email and password are in .env file, quitting..."
    );
    process.exit(0);
  }
};
verifyEnvs(email, password, deviceId);

console.log(`${email} attempting to authenticate to ${deviceId}`);

const main = async () => {
  const neurosity = new Neurosity({
    deviceId,
  });
  await neurosity
    .login({
      email,
      password,
    })
    .catch((error) => {
      console.log(error);
      throw new Error(error);
    });
  console.log("Logged in");

  // DO STUFF HERE

  // const neurosity = new Neurosity();
  neurosity.brainwaves("raw").subscribe((brainwaves) => {
    console.log(brainwaves);
  });

  neurosity.calm().subscribe((calm) => {
    console.log(calm.probability);
    if (calm.probability > 0.3) {
      console.log("Hello World!");
    }
  });
};

main();
