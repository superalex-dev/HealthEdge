FROM node:16 AS build
WORKDIR /app
COPY healthedgefrontend/package.json healthedgefrontend/package-lock.json ./
RUN npm install
COPY healthedgefrontend/ ./
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]