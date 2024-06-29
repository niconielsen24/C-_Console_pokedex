# PokeCLI

PokeCLI is a command-line interface (CLI) application for interacting with a Pokémon database. It allows users to query information about Pokémon and their moves.

### Usage
    ```bash
    dotnet run -- [OPTIONS]
        -h, --help                     : Shows this help message and exit.
        -p <name> , --pokemon <name>   : Finds data related to that pokemon, shows it and exit.
        -i <name> , --id_num <name>    : Finds data related to that pokemon id, shows it and exit.
    ```

- Also, the app assumes the database is running, and is in the standard 3306 port.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [MySql .net package](https://www.nuget.org/packages/MySql.Data/)

This project actually uses the docker MySQL image I used in another repository, so you should checkout it out.

- https://github.com/niconielsen24/mysql_poke_db

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/niconielsen24/C-_Console_pokedex.git
