# Use an official Node.js runtime as a parent image
FROM node

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install app dependencies
RUN npm install

# Copy the rest of the application code to the container
COPY . .

# Expose a port that the application will run on (e.g., 3000)
EXPOSE 5173

# Start the Vite development server
CMD ["npm", "run", "dev"]