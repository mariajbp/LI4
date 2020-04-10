from flask import Flask , jsonify
from flask_restful import Api
from flask_sqlalchemy import SQLAlchemy
from flask_jwt_extended import JWTManager
from werkzeug.exceptions import HTTPException
from common.responses import unauthorized

app = Flask(__name__)
api = Api(app)

jwt = JWTManager(app)
jwt.invalid_token_loader(unauthorized)

app.config['JWT_SECRET_KEY'] = '1430b4f944d890bd4eed14421e5a31b595d3e11d12fd59fb4c419c2f909045ef'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:root@localhost/ticketnow'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['BASE_ENDPOINT'] = '/api'

db = SQLAlchemy(app)