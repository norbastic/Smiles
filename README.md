# README

## Requirements
The solution uses postgres container as DB engine, if you don't have please issue the follwoing command:

    ```$ docker run -p 5432:5432 --name mypostgresdb -e POSTGRES_PASSWORD=Password1 -d postgres```

API Project is the startup project, you can start it with the following command:

    ```$ cd Smiles.API
    $ dotnet run```

Angular as CLI app, which requires node.js

    ```$ cd ./Smiles.CLI
    $ npm install
    $ npm start```