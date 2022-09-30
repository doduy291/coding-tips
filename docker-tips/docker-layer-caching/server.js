const express = require("express");
const http = require("http");

const app = express();

const port = 8000;
const server = http.createServer(app);

app.get("/", (req, res) => {
  res.send("Hello");
});

server.listen(port, () => console.log(`App running on ${port}`));
