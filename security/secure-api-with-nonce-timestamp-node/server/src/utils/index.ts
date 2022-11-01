import md5 from "md5";

export interface Params {
  stime: number;
  nonce: string | number;
  signature: string;
  team: string;
  version: string;
  token: string;
}
const generateServerSignature = (params: Params) => {
  const token = "xxxxYYYY";
  params.token = token;
  params.version = "v1";

  const sortKeys = [];
  for (const key in params) {
    if (key !== "signature") {
      sortKeys.push(key);
    }
  }
  sortKeys.sort(); // sort of ASCII

  let paramsHolder = "";
  sortKeys.forEach((key) => {
    paramsHolder += key + params[key as keyof Params]; // ex: teamteamAsignaturexxxxyyyy...
  });

  return md5(paramsHolder).toString();
};

export { generateServerSignature };
