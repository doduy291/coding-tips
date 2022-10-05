This is a sample build. Optimize docker image should only be built in production enviroment.

```bash
# Install dependencies only when needed
FROM node:14-alpine AS deps

WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn install --frozen-lockfile

# Rebuild the source code only when needed
FROM node:14-alpine AS builder
WORKDIR /app
COPY . .
COPY --from=deps /app/node_modules ./node_modules
RUN yarn build && yarn install --production --ignore-scripts

# Production image, copy all the files and run next
FROM node:14-alpine AS runner
WORKDIR /app

ENV NODE_ENV production

# Create and add user
RUN addgroup -g 1001 -S nodejs
RUN adduser -S nextjs -u 1001

COPY --from=builder /app/public ./public
COPY --from=builder --chown=nextjs:nodejs /app/.next ./.next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package.json ./package.json

USER nextjs

CMD ["yarn", "start"]
```

## Yarn Options

`yarn install --frozen-lockfile`: Don’t generate a yarn.lock lockfile and fail if an update is needed. <br/>
`yarn install --production`: Yarn will not install any package listed in devDependencies. <br/>
`yarn install --ignore-scripts`: Do not execute any scripts defined in the project package.json and its dependencies. <br/>

## Reference

https://viblo.asia/p/tang-toc-do-build-va-toi-gian-docker-image-Eb85oODB52G
