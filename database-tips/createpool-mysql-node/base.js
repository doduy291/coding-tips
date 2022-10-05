import express from "express";
import http from "http";
import mysql from "mysql2";

const app = express();

function getConnection() {
  return mysql.createConnection({
    host: "127.0.0.1",
    user: "user",
    password: "password",
    database: "pooltest",
    insecureAuth: true,
  });
}

app.get("/", (req, res) => {
  res.send("API is running");
});

app.get("/old-connection-way", (req, res) => {
  const conn = getConnection();
  conn.connect((err) => {
    if (err) {
      console.error("Error Connection: ", err);
      return res.send("Error Connection");
    }
    conn.query("select * from users limit 5", (error, records) => {
      if (error) {
        console.log(`error::`, error);
        res.send(error);
      }
      console.log(`records:::`, records);
      res.send("Using createConnection");
      conn.end();
    });
  });
});

const port = 8000;
const server = http.createServer(app);
server.listen(port, () => console.log(`App running on port ${port}`));
