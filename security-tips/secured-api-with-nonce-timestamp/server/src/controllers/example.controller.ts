import { Request, Response, NextFunction, RequestHandler } from "express";
import { generateServerSignature, Params } from "../utils";

interface Teams {
  [key: string]: Array<object>;
}
const TIME_REQUIREMENT = 30; // seconds
const teams: Teams = {
  teamA: [
    {
      name: "cr7",
      number: "7",
    },
    {
      name: "m10",
      number: "10",
    },
  ],
  teamB: [
    {
      name: "cr8",
      number: "8",
    },
    {
      name: "m11",
      number: "11",
    },
  ],
};

const getAllExample = async (
  req: Request,
  res: Response,
  next: NextFunction,
) => {
  try {
    const { stime, signature, nonce, team } = req.query;
    if (!stime || !signature || !nonce) {
      return res.status(400).json({
        status: "error",
        message: "bad request",
      });
    }

    // compare time
    // * (Should be called from client side)
    const isTime = Math.floor((Date.now() - Number(stime)) / 1000); // ~ seconds
    if (isTime > TIME_REQUIREMENT) {
      return res.status(401).json({
        status: "error",
        message: "expired",
      });
    }

    const serverSignature = generateServerSignature(
      req.query as unknown as Params,
    );
    console.log(`Server Sign: ${serverSignature}`);
    console.log(`Sign: ${signature}`);
    if (serverSignature !== signature) {
      return res.status(401).json({
        status: "error",
        message: "sign invalid",
      });
    }

    return res.status(200).json({
      status: "success",
      elements: teams[team as keyof Teams],
    });
  } catch (error) {
    next(error);
  }
};

export { getAllExample };
