const sideA = 10; // key secret of A side
const sideB = 5; // key secret of B side

const keySecretSystem = 23; // key secret of system

const sideA_keyPublic = sideA + keySecretSystem;

const sideB_keyPublic = sideB + keySecretSystem;

// When one side wanna to read message from that side
const sideA_keyCommon = sideB_keyPublic + sideA;
console.log(`Side A common key: ${sideA_keyCommon}`);

const sideB_keyCommon = sideA_keyPublic + sideB;
console.log(`Side B common key: ${sideB_keyCommon}`);

// Check equal
console.log(sideA_keyCommon === sideB_keyCommon);
