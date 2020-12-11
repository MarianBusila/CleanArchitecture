# CleanArchitecture

## Overview

A clean architecture implementation based on this project https://github.com/drminnaar/chinook.

## Getting started

### Requirements
- docker
- docker-compose
- dotnet core 3.1

### Database setup
Postgres is used to store the data releated to the service.
-  when run on Windows docker, the **data/migrations** folder must be shared. 
Go to Settings -> Resources -> File sharing in Docker desktop application and add this folder.
- run script to create the database and populate with data

```
# create database and apply migrations
./refresh-chinook-db.ps1
```

### Run dependencies
- postgres
- pgAdmin

```
docker-compose up
```
