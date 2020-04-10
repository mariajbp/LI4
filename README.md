# LI4 Project

## Index

* [Server](#Server)
    * [Installing](#Installing)
    * [Executing](#Executing)

## Server

### Installing

Required: MySQL / MariaDB client

```shell
cd TicketNow-Server

python3 -m venv ticketnow-env

# Windows
ticketnow-env\Scripts\activate.bat

# Unix / MacOS
source ticketnow-env/bin/activate

pip install -r requirements.txt
```

### Executing

```shell
# Preferentially with super user permitions
python3 src/app.py
```