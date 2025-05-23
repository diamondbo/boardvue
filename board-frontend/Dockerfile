# Stage 1: Build the app
FROM node:20-alpine AS build

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the app
COPY . .

# Build the app
RUN npm run build


# Stage 2: Serve the app with Nginx
FROM nginx:alpine

# Replace the default Nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy built Vue app
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]