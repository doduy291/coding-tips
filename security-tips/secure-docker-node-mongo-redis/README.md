## Ways to secure Docker app

### Docker network

`Docker network` keeps from accessing other services that it does not rely on

```bash
// docker-compose.yml

services:
  app:
    ...
    networks:
      - db-network
      - redis-network

  db:
    ...
    networks:
      - db-network

  redis:
    ...
    networks:
      - redis-network

networks:
  #  default =====> driver: bridge
  db-network:
  redis-network:
```

### Authentication for Mongodb and Redis

**1. Mongodb**
Create file `db-entrypoint.sh` in `.docker` folder with following content

```bash
echo 'Creating application user and db'

mongosh --host localhost --port ${DB_PORT} \
  -u ${MONGO_INITDB_ROOT_USERNAME} \
  -p ${MONGO_INITDB_ROOT_PASSWORD} \
  --authenticationDatabase admin \
  --eval "use ${DB_NAME}" \
  --eval "db.createUser({user: '${DB_USER}', pwd: '${DB_PASSWORD}', roles:[{role:'dbOwner', db: '${DB_NAME}'}]});"
```

> `mongosh` command is used from Mongo version 6.0
> All template strings are got from `.env` file

Supplement more enviroment variables for MongoDB in `.env` file

```bash
DB_HOST=db
DB_PORT=27017
DB_NAME=my_db
DB_ROOT_USER=rootuser
DB_ROOT_PASS=rootuserpass
DB_USER=myuser
DB_PASSWORD=myuserpass
```

At `docker-compose.yml`, modify `app` and `db` service like this:

```bash
services:
  app:
    ...
    env_file: .env
    ...

  db:
    ...
    volumes:
      - .docker/data/db:/data/db
      - .docker/db-entrypoint.sh:/docker-entrypoint-initdb.d/db-entrypoint.sh
    ...
    environment:
      - MONGO_INITDB_ROOT_USERNAME=${DB_ROOT_USER}
      - MONGO_INITDB_ROOT_PASSWORD=${DB_ROOT_PASS}
      - DB_PORT=${DB_PORT}
      - DB_NAME=${DB_NAME}
      - DB_USER=${DB_USER}
      - DB_PASSWORD=${DB_PASSWORD}
    ...
```

Finally, change some code in `app.js`

```bash
...
const dbHost = process.env.DB_HOST || 'localhost'
const dbPort = process.env.DB_PORT || 27017
const dbName = process.env.DB_NAME || 'my_db_name'
const dbUser = process.env.DB_USER
const dbUserPassword = process.env.DB_PASSWORD
const mongoUrl = `mongodb://${dbUser}:${dbUserPassword}@${dbHost}:${dbPort}/${dbName}`
...
```

**2. Redis**
Supplement more enviroment variables for Redis in `.env` file

```bash
...
REDIS_HOST=redis
REDIS_PORT=6379
REDIS_PASSWORD=redispass
```

At `docker-compose.yml`, modify `app` and `redis` service like this:

```bash
services:
  app:
    ...
    env_file: .env
    ...

  db:
    ...

  redis:
    ...
    command: redis-server --requirepass ${REDIS_PASSWORD}
    ...
```

After, add more config in `./modules/session.js`

```bash
...
const client = redis.createClient({
  host: process.env.REDIS_HOST || "127.0.0.1",
  port: process.env.REDIS_PORT || 6379,
  password: process.env.REDIS_PASSWORD,
});
...
```

### Non-root user

Supplement content in `Dockerfile` file

```bash
...
# Production
RUN npm install -g pm2

# Change directory permission for our app
RUN chown -R node:node /app
# Tell docker that all future commands should run as the "node" user
USER node

CMD ["pm2-runtime", "ecosystem.config.js", "--env", "production"]
```

> "node:node" user is default user that is created in app

Or, we can create new own user like this content

```bash
...
# Production
RUN npm install -g pm2

# Create a group
RUN addgroup -S appgroup
# Create a user and make it join "appgroup"
RUN adduser -S appuser -G appgroup
# Then change directory permission for our app
RUN chown -R appuser:appgroup /app
# Tell docker that all future commands should run as the "appuser" user
USER appuser

CMD ["pm2-runtime", "ecosystem.config.js", "--env", "production"]
```

Some commands to check permission

```bash
# Run app shell
$ docker-compose exec app sh

# Check current directory permission
$ ls -l

# List user in service
$ cat /etc/passwd
```

## Install and run

> Make sure you installed `Docker`

```bash
# Build image
$ docker build -t <YOUR_NAME_IMAGE>:node .

# Build container
$ docker-compose up -d
```

## Reference

https://viblo.asia/p/bao-mat-ung-dung-docker-nodejs-mongo-redis-GrLZDaaVlk0
His github: https://github.com/maitrungduc1410
