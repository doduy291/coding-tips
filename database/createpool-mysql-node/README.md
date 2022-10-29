## Getting started

```bash
# Install dependencies
$ npm install

# Install Mysql via Docker
$ docker-compose up --build -d

# Run server
$ node pool.js
```

## Test Command

```bash
# Open MySql shell
$ docker-compose exec db bash

# Login
$ mysql -u user -p
# Then just type password in visibility, example:
$ Enter password: password

# Test after sending request
$ show processlist;
```

## Some information

https://stackoverflow.com/questions/45522907/how-does-connectionlimit-work-in-node-mysql
