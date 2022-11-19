const crypto = require("node:crypto");

async function generateAES(length = 256) {
  const key = await crypto.subtle.generateKey(
    {
      name: "AES-CBC",
      length,
    },
    true,
    ["encrypt", "decrypt"],
  );
  return key;
}

async function encryptAES(plaintext) {
  const ec = new TextEncoder();
  const key = await generateAES();
  const iv = crypto.getRandomValues(new Uint8Array(16));
  const ciphertext = await crypto.subtle.encrypt(
    { name: "AES-CBC", iv },
    key,
    ec.encode(plaintext),
  );

  return { key, iv, ciphertext };
}

async function decryptAES(ciphertext, key, iv) {
  const dec = new TextDecoder();
  const plaintext = await crypto.subtle.decrypt(
    {
      name: "AES-CBC",
      iv,
    },
    key,
    ciphertext,
  );
  return dec.decode(plaintext);
}

async function message(msg) {
  const { key, iv, ciphertext } = await encryptAES(msg);
  console.log(`encrypt::`, ciphertext);
  console.log(`decrypt::`, await decryptAES(ciphertext, key, iv));
}

message("Yay!!! CRYPTO");
