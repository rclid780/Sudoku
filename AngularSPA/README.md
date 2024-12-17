# SudokuAngular

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.0.4.

## Development server

To start a local server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`.<br>
Click New Game to load the Sudoku puzzle.<br>
Click on the square you want to update and select the number 1-9 on the right to update the square.<br>
Click the '-' on the right to remove a value you think is incorrect<br>
Squares with a light gray background can not be changed since they are part of the puzzle<br>
Clicking check solution will start at the top left square (0,0) and move left to right, top to bottom checking squares
check solution will stop and alert on the first incorrect of empty value. This will gives minimal hints.<br>
Checking Highlight Errors will set the background of any incorrect square to red this intended for beginners learning Sudoku<br>

Note: there is currently only one puzzle clicking New Game a second time relods the current puzzle from the start position<br>
A C# backend service is being coded to generate and serve new puzzles and solutions to this SPA<br>

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
