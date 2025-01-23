FROM node:22.11-alpine3.19 as builder
WORKDIR /app

COPY ./package*.json ./
RUN npm install

COPY . .

RUN npm run build

FROM node:22.11-alpine3.19

COPY --from=builder /app/.output ./.output

EXPOSE 3000

CMD [ "node", ".output/server/index.mjs" ]
