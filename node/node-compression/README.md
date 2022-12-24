```js
const express = require("express");
const compression = require("compression");
const app = express();

app.use(
  compression({
    level: 6, // 6 is best optimize, it'll help server reduce memory to handle compression
    threshold: 100 * 1000 // convert to byte => (100 byte * 1000 = 100 KB) (if Size in network > 100kb, compression will be implemented)
    // Just be filter
    filter: (req, res) => {
      if (req.headers["x-no-compression"]) {
        // don't compress responses with this request header
        return false;
      }
      // fallback to standard filter function
      return compression.filter(req, res);
    },
  }),
  }),
);

app.get("/", (req, res) => {
  const text = "Test";
  res.send(text.repeat(100000));
});

app.listen(8080, () => {
  console.log("Server is running on port 8080");
});
```
