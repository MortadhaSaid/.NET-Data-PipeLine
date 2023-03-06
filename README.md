# a .NET pipeline
Objective
made to send data from small componants , the "Pipe Clients" to a "Pipe Server" using .NET pipelines while being multithreaded
## Componants
*PipeClient: The small componant responsible for having the data and trasnferring it to the server
*PipeServer: the main componant which will be listening to and accepting data transfer from "Clients" which can also contain the Business Logic
*//TO_BE_ADDED\\ a script file that auto launches client on demand
## Requirements
.NET 3.5(at the least)
## Setup
1. Clone the repository
```sh
git clone https://github.com/<username>/<repository-name>.git
```
2. Navigate to the project directory
```sh
cd <repository-name>
```
3. Build the solution
```sh
dotnet build
```
