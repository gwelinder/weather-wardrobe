# Weather Wardrobe

A smart wardrobe application that provides clothing recommendations based on weather conditions. Built with Angular and .NET Core.

## Features

- Get clothing recommendations based on location
- Simulated weather data generation
- Admin dashboard for managing clothing items
- Responsive design for all devices

## Tech Stack

- **Frontend**: Angular 18.2.7
- **Backend**: .NET 8
- **Database**: PostgreSQL
- **API**: RESTful endpoints

## Prerequisites

- Node.js and npm
- .NET 8 SDK
- PostgreSQL
- Angular CLI

## Getting Started

1. Clone the repository:
   ```bash
   git clone git@github.com:gwelinder/weather-wardrobe.git
   cd weather-wardrobe
   ```

2. Install Frontend Dependencies:
   ```bash
   npm install
   ```

3. Install Backend Dependencies:
   ```bash
   cd WeatherWardrobeApi
   dotnet restore
   ```

4. Set up the Database:
   - Create a PostgreSQL database
   - Update the connection string in `appsettings.json`
   - Run migrations:
     ```bash
     dotnet ef database update
     ```

5. Start the Backend:
   ```bash
   dotnet run
   ```

6. Start the Frontend:
   ```bash
   ng serve
   ```

7. Open your browser and navigate to `http://localhost:4200`

## Project Structure

- `/src` - Angular frontend application
- `/WeatherWardrobeApi` - .NET Core backend application
  - `/Controllers` - API endpoints
  - `/Models` - Data models
  - `/Data` - Database context and migrations

## Development

### Frontend Development

- Run `ng serve` for a dev server
- Navigate to `http://localhost:4200/`
- The application will automatically reload if you change any source files

### Backend Development

- Run `dotnet run` from the WeatherWardrobeApi directory
- API will be available at `http://localhost:5055`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
