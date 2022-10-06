import Express from "express";
import { getAllExample } from "../controllers/example.controller";
const router = Express.Router();

router.get("/", getAllExample);

export default router;
