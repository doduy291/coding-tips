import express from "express";
import http from "http";
import mysql from "mysql2";

const app = express();

const pool = mysql.createPool({
  host: "127.0.0.1",
  user: "user",
  password: "password",
  database: "pooltest",
  connectionLimit: 10,
});

app.get("/", (req, res) => {
  res.send("API is running");
});

// # Case: one query
// *** With one query, connection will automatically close ***
app.get("/pool-one-query", (req, res) => {
  pool.query("select * from users limit 5", (error, records) => {
    if (error) {
      console.log(`error::`, error);
      res.send(error);
    }
    console.log(`records:::`, records);
    res.send("Using createPool with one query");
  });
});

// # Case: multi query
app.get("/pool-multi-query", (req, res) => {
  pool.getConnection((err, conn) => {
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
    });
    conn.query("select * from users limit 3", (error, records) => {
      if (error) {
        console.log(`error::`, error);
        res.send(error);
      }
      console.log(`records:::`, records);
    });
    res.send("Using createPool with multi query");
    // Release (close) connection
    pool.releaseConnection(conn);
  });
});

const port = 8000;
const server = http.createServer(app);
server.listen(port, () => console.log(`App running on port ${port}`));
