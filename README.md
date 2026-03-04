# Agent8

A .NET 8 Razor Pages project for building web applications.

## Project Overview

This project demonstrates a simple Todo application built with ASP.NET Core Razor Pages. It follows the Model-View-Controller (MVC) pattern with a clear separation of concerns between the model, service, and presentation layers.

## Architecture Overview

The application is structured into three main layers:

1. **Model Layer** (`Models/` folder)
   - Contains data models such as `TodoItem`
   - Defines the structure of data entities

2. **Service Layer** (`Services/` folder)
   - Contains business logic in `TodoService`
   - Handles data operations and interactions with the data store

3. **Presentation Layer** (`Pages/` folder)
   - Contains Razor Pages for the UI
   - Includes pages for displaying, creating, editing, and deleting todo items

## Getting Started

To run this application locally:

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet restore` to restore dependencies
4. Run `dotnet run` to start the development server

## Running Tests

To run the tests:

1. Navigate to the project directory
2. Run `dotnet test` to execute all tests

## Building the Project

To build the project:

1. Navigate to the project directory
2. Run `dotnet build` to compile the application

## Usage Guidelines

- Use the web interface to manage todo items
- The application provides CRUD (Create, Read, Update, Delete) functionality for todo items
- All data is stored in-memory for demonstration purposes