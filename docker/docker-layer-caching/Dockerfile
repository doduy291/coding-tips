FROM node:18-alpine AS development

WORKDIR /usr/src/app 

COPY package*.json .

RUN npm install

COPY . .

USER node

EXPOSE 8000