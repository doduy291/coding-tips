import express from "express";
import cors from "cors";
import http from "http";
import dotenv from "dotenv";
import morgan from "morgan";

// Router Imports
import exampleRouter from "./src/routers/example.router";
// Configs
const app = express();
dotenv.config({ path: ".env.development" });

// Middlewares
app.use(cors());
app.use(morgan("short"));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// API routes
app.get("/", (req, res) => {
  res.send("API is running");
});
app.use("/api/example", exampleRouter);

// Listen
const port = process.env.PORT;
const server = http.createServer(app);

server.listen(port, () => console.log(`App running on port ${port}`));
