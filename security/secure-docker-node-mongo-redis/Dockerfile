FROM node:12.18-alpine

WORKDIR /app

COPY . .

RUN npm install

# Production
RUN npm install -g pm2

# Change directory permission for our app
RUN chown -R node:node /app
# Tell docker that all future commands should run as the "node" user
USER node

CMD ["pm2-runtime", "ecosystem.config.js", "--env", "production"]
