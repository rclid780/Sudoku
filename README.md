# Sudoku

## Angular SPA
This is a light weight single page app designed to display sudoku puzzles to be solved
- Currently only one puzzle is supported
- Highlighting of errors in the solution can be turned on to add in solving if needed
- The plan is to either implement the shared dll in javascript or connect this to the backend C# sever

## BlazorWAS
This is a fully functional WebAssembly
- Will generate new puzzle
- Highlighting of errors in the solution can be turned on to add in solving if needed
- uses Local Storage to maintain state of the puzzle
- demonstrates navigation
- counter on demo navigation page does not support state matenance since it is just a demo
  
## Shared
This is the C# library code for Generating and Solving Sudoku puzzles

## WebServer
The web server is a web api that is backed by EF using Sqlite as the database
when run the server will create a new databse in the same directory as the exe if one doesn't exist

## planned projects
- Vue.js SPA
- Backend minimal API
- Backend fast API
- Blazor Server Side
